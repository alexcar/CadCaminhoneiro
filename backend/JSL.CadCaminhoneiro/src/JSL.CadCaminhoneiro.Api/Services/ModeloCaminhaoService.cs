using JSL.CadCaminhoneiro.Api.Dto;
using JSL.CadCaminhoneiro.Api.Infrastructure.Notifications;
using JSL.CadCaminhoneiro.Domain.Entities;
using JSL.CadCaminhoneiro.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JSL.CadCaminhoneiro.Api.Services
{
    public class ModeloCaminhaoService : IModeloCaminhaoService
    {
        private readonly NotificationContext _notificationContext;
        private readonly IModeloCaminhaoRepository _repository;
        private readonly IMarcaCaminhaoRepository _marcaCaminhaoRepository;

        public ModeloCaminhaoService(
            NotificationContext notificationContext,
            IModeloCaminhaoRepository repository,
            IMarcaCaminhaoRepository marcaCaminhaoRepository)
        {
            _notificationContext = notificationContext;
            _repository = repository;
            _marcaCaminhaoRepository = marcaCaminhaoRepository;
        }

        public async Task<IEnumerable<ModeloCaminhao>> ListarTodosAsync(string ordenacao)
        {
            return await _repository.ListarTodosAsync(ordenacao);
        }

        public async Task<Guid> IncluirAsync(ModeloCaminhaoIncluirRequest entity)
        {
            var marcaCaminhao = await _marcaCaminhaoRepository.ObterPorIdAsync(entity.MarcaCaminhaoId);

            if (marcaCaminhao is null)
            {
                _notificationContext.AddNotification(new Notification("MarcaCaminhaoId",
                        $"A marca de caminhão com o Id: {entity.MarcaCaminhaoId} não existe"));
                return Guid.Empty;
            }

            var modeloJaExiste = await _repository.ObterPorDescricaoAnoAsync(entity.Descricao, entity.Ano);

            if (modeloJaExiste != null)
            {
                _notificationContext.AddNotification(new Notification("Descricao",
                        $"O modelo de caminhão com a descrição: {entity.Descricao} já existe"));
                return Guid.Empty;
            }
            
            var modeloCaminhao = new ModeloCaminhao(entity.Descricao, entity.Ano, marcaCaminhao.Id);
            modeloCaminhao.Incluir(DateTime.Now);

            await _repository.IncluirAsync(modeloCaminhao);

            return modeloCaminhao.Id;
        }

        public async Task AlterarAsync(ModeloCaminhaoAlterarRequest entity)
        {
            var marcaCaminhao = await _marcaCaminhaoRepository.ObterPorIdAsync(entity.MarcaCaminhaoId);

            if (marcaCaminhao is null)
            {
                _notificationContext.AddNotification(new Notification("MarcaCaminhaoId",
                        $"A marca de caminhão com o Id: {entity.MarcaCaminhaoId} não existe"));
                return;
            }

            var modeloCaminhaoOriginal = await _repository.ObterOriginalAsync(entity.Id);

            // Se diferente, valida duplicidade
            if (!modeloCaminhaoOriginal.Descricao.Trim().Equals(entity.Descricao.Trim(), StringComparison.OrdinalIgnoreCase) ||
                !modeloCaminhaoOriginal.Ano.Trim().Equals(entity.Ano.Trim(), StringComparison.OrdinalIgnoreCase))
            {
                // Verificar se a nova descrição já existe
                var modeloJaExiste = await _repository.ObterPorDescricaoAnoAsync(entity.Descricao, entity.Ano);

                if (modeloJaExiste != null)
                {
                    _notificationContext.AddNotification(new Notification("Descricao",
                            $"O modelo de caminhão com a descrição: {entity.Descricao} já existe"));
                    return;
                }
            }

            modeloCaminhaoOriginal.Alterar(entity.Descricao, entity.Ano, entity.MarcaCaminhaoId, DateTime.Now);
            await _repository.AlterarAsync(modeloCaminhaoOriginal);
        }

        public async Task ExcluirAsync(ModeloCaminhao entity)
        {
            await _repository.ExcluirAsync(entity);
        }
    }
}
