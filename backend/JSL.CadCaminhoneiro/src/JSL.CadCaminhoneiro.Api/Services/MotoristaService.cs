using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using JSL.CadCaminhoneiro.Api.Dto;
using JSL.CadCaminhoneiro.Api.Infrastructure.Notifications;
using JSL.CadCaminhoneiro.Domain.Entities;
using JSL.CadCaminhoneiro.Domain.Interfaces;

namespace JSL.CadCaminhoneiro.Api.Services
{
    public class MotoristaService : IMotoristaService
    {
        private readonly IMotoristaRepository _repository;
        private readonly NotificationContext _notificationContext;
        private readonly IModeloCaminhaoRepository _modeloCaminhaoRepository;
        private readonly IMarcaCaminhaoRepository _marcaCaminhaoRepository;

        // TODO: Criar exceção e verificar se o log vai registrar o erro.

        public MotoristaService(
            IMotoristaRepository repository,
            IMarcaCaminhaoRepository marcaCaminhaoRepository,
            IModeloCaminhaoRepository modeloCaminhaoRepository,
            NotificationContext notificationContext)
        {
            _repository = repository;
            _marcaCaminhaoRepository = marcaCaminhaoRepository;
            _modeloCaminhaoRepository = modeloCaminhaoRepository;
            _notificationContext = notificationContext;
        }

        public async Task<IEnumerable<Motorista>> ListarTodosAsync(string ordenacao)
        {
            return await _repository.ListarTodosAsync(ordenacao);
        }

        public async Task<Guid> IncluirAsync(MotoristaIncluirRequest entity)
        {
            var marcaCaminhao = await _marcaCaminhaoRepository.ObterPorIdAsync(entity.MarcaCaminhaoId);

            if (marcaCaminhao is null)
            {
                _notificationContext.AddNotification(new Notification("MarcaCaminhaoId",
                        $"A marca de caminhão com o Id: {entity.MarcaCaminhaoId} não existe"));
                return Guid.Empty;
            }

            var modeloCaminhao = await _modeloCaminhaoRepository.ObterPorIdAsync(entity.ModeloCaminhaoId);

            if (modeloCaminhao is null)
            {
                _notificationContext.AddNotification(new Notification("ModeloCaminhaoId",
                        $"O modelo de caminhão com o Id: {entity.MarcaCaminhaoId} não existe"));
                return Guid.Empty;
            }

            // Verifica se motorista já existe
            // por cpf
            var existeMotoristaPorCpf = await _repository.ExistePorCpfAsync(entity.Cpf);

            if (existeMotoristaPorCpf)
            {
                _notificationContext.AddNotification(new Notification("Cpf",
                        $"Motorista com o Cpf: {entity.Cpf} já existe"));
                return Guid.Empty;
            }

            // por RG
            var existeMotoristaPorRg = await _repository.ExistePorNumeroRegistroGeralAsync(entity.NumeroRegistroGeral);

            if (existeMotoristaPorRg)
            {
                _notificationContext.AddNotification(new Notification("NumeroRegistroGeral",
                        $"Motorista com o RG: {entity.Cpf} já existe"));
                return Guid.Empty;
            }

            // por Registro CHN
            var existeMotoristaPorChn = await _repository.ExistePorNumeroRegistroHabilitacaoAsync(entity.NumeroRegistroHabilitacao);

            if (existeMotoristaPorChn)
            {
                _notificationContext.AddNotification(new Notification("NumeroRegistroHabilitacao",
                        $"Motorista com a CHN: {entity.Cpf} já existe"));
                return Guid.Empty;
            }                       

            var motorista = new Motorista(
                entity.Nome,
                entity.Cpf,
                entity.DataNascimento,
                entity.NomePai,
                entity.NomeMae,
                entity.Naturalidade,
                entity.NumeroRegistroGeral,
                entity.OrgaoExpedicaoRegistroGeral,
                entity.DataExpedicaoRegistroGeral);                        
            motorista.Incluir(DateTime.Now);

            var endereco = new Endereco(
                entity.Logradouro, entity.Numero, entity.Complemento,
                entity.Bairro, entity.Municipio, entity.Uf, entity.Cep, motorista.Id);
            endereco.Incluir(DateTime.Now);            
            motorista.IncluirEndereco(endereco);

            var habilitacao = new Habilitacao(
                entity.NumeroRegistroHabilitacao, entity.CategoriaHabilitacao,
                entity.DataPrimeiraHabilitacao, entity.DataValidadeHabilitacao,
                entity.DataEmissaoHabilitacao, entity.ObservacaoHabilitacao, motorista.Id);
            habilitacao.Incluir(DateTime.Now);
            motorista.IncluirHabilitacao(habilitacao);

            var caminhao = new Caminhao(
                entity.Placa, entity.Eixo, entity.CaminhaoObservacao,
                marcaCaminhao.Id, modeloCaminhao.Id, motorista.Id);
            caminhao.Incluir(DateTime.Now);
            motorista.IncluirCaminhao(caminhao);

            await _repository.IncluirAsync(motorista);

            return motorista.Id;
        }

        public async Task AlterarAsync(MotoristaAlterarRequest entity)
        {
            var marcaCaminhao = await _marcaCaminhaoRepository.ObterPorIdAsync(entity.MarcaCaminhaoId);

            if (marcaCaminhao is null)
            {
                _notificationContext.AddNotification(new Notification("MarcaCaminhaoId",
                        $"A marca de caminhão com o Id: {entity.MarcaCaminhaoId} não existe"));
                return;
            }

            var modeloCaminhao = await _modeloCaminhaoRepository.ObterPorIdAsync(entity.ModeloCaminhaoId);

            if (modeloCaminhao is null)
            {
                _notificationContext.AddNotification(new Notification("ModeloCaminhaoId",
                        $"O modelo de caminhão com o Id: {entity.MarcaCaminhaoId} não existe"));
                return;
            }

            var motorista = await _repository.ObterPorIdAsync(entity.Id);
            
            // Se cpf diferente, valida duplicidade
            if (entity.Cpf.Trim() != motorista.Cpf.Trim())
            {
                // Verifica se o novo cpf já existe
                var existeMotoristaPorCpf = await _repository.ExistePorCpfAsync(entity.Cpf.Trim());

                if (existeMotoristaPorCpf)
                {
                    _notificationContext.AddNotification(new Notification("Cpf",
                            $"Motorista com o Cpf: {entity.Cpf} já existe"));
                    return;
                }
            }

            // Se RG diferente, valida duplicidade
            if (entity.NumeroRegistroGeral.Trim() != motorista.NumeroRegistroGeral.Trim())
            {
                // Verifica se o novo RG já existe
                var existeMotoristaPorRg = await 
                    _repository.ExistePorNumeroRegistroGeralAsync(entity.NumeroRegistroGeral.Trim());

                if (existeMotoristaPorRg)
                {
                    _notificationContext.AddNotification(new Notification("NumeroRegistroGeral",
                            $"Motorista com o RG: {entity.Cpf} já existe"));
                    return;
                }
            }

            // Se CHN diferente, valida duplicidade
            if (entity.NumeroRegistroHabilitacao.Trim() != motorista.Habilitacao.NumeroRegistro.Trim())
            {
                // Verifica se a nova CHN já existe
                var existeMotoristaPorChn = await 
                    _repository.ExistePorNumeroRegistroHabilitacaoAsync(entity.NumeroRegistroHabilitacao.Trim());

                if (existeMotoristaPorChn)
                {
                    _notificationContext.AddNotification(new Notification("NumeroRegistroHabilitacao",
                            $"Motorista com a CHN: {entity.Cpf} já existe"));
                    return;
                }
            }

            motorista.Endereco.Alterar(
                entity.Logradouro, entity.Numero, entity.Complemento,
                entity.Bairro, entity.Municipio, entity.Uf, entity.Cep, DateTime.Now);            

            motorista.Habilitacao.Alterar(
                entity.NumeroRegistroHabilitacao, entity.CategoriaHabilitacao,
                entity.DataPrimeiraHabilitacao, entity.DataValidadeHabilitacao,
                entity.DataEmissaoHabilitacao, entity.ObservacaoHabilitacao, DateTime.Now);

            motorista.Caminhao.Alterar(
                entity.Placa, entity.Eixo, entity.CaminhaoObservacao,
                marcaCaminhao.Id, modeloCaminhao.Id, DateTime.Now);            

            motorista.Alterar(
                entity.Nome,
                entity.Cpf,
                entity.DataNascimento,
                entity.NomePai,
                entity.NomeMae,
                entity.Naturalidade,
                entity.NumeroRegistroGeral,
                entity.OrgaoExpedicaoRegistroGeral,
                entity.DataExpedicaoRegistroGeral,
                DateTime.Now);
            await _repository.AlterarAsync(motorista);
        }

        public async Task ExcluirAsync(Motorista entity)
        {
            await _repository.ExcluirAsync(entity);
        }        
    }
}
