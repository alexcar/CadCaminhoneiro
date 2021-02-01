using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using JSL.CadCaminhoneiro.Api.Dto;
using JSL.CadCaminhoneiro.Api.Infrastructure.Notifications;
using JSL.CadCaminhoneiro.Domain.Entities;
using JSL.CadCaminhoneiro.Domain.Interfaces;

namespace JSL.CadCaminhoneiro.Api.Services
{
    public class MarcaCaminhaoService : IMarcaCaminhaoService
    {
        private readonly NotificationContext _notificationContext;
        private readonly IMarcaCaminhaoRepository _repository;

        public MarcaCaminhaoService(
            NotificationContext notificationContext,
            IMarcaCaminhaoRepository repository)
        {
            _notificationContext = notificationContext;
            _repository = repository;
        }

        public async Task<IEnumerable<MarcaCaminhao>> ListarTodosAsync(string ordenacao)
        {
            return await _repository.ListarTodosAsync(ordenacao);
        }

        public async Task<Guid> IncluirAsync(MarcaCaminhaoIncluirRequest entity)
        {
            // Verificar se a nova descrição já existe
            await ValidarDescricao(entity.Descricao);

            if (_notificationContext.HasNotifications)
                return Guid.Empty;  
            
            var marcaCaminhao = new MarcaCaminhao(entity.Descricao);
            marcaCaminhao.Incluir(DateTime.Now);

            await _repository.IncluirAsync(marcaCaminhao);

            return marcaCaminhao.Id;
        }

        public async Task AlterarAsync(MarcaCaminhaoAlterarRequest entity)
        {
            var marcaCaminhao = await _repository.ObterOriginalAsync(entity.Id);

            // Se diferente, valida duplicidade
            if (!marcaCaminhao.Descricao.Trim().Equals(entity.Descricao.Trim(), StringComparison.OrdinalIgnoreCase))
            {
                // Verificar se a nova descrição já existe
                await ValidarDescricao(entity.Descricao);
            }            

            if (!_notificationContext.HasNotifications)
            {
                marcaCaminhao.Alterar(entity.Descricao, DateTime.Now);
                await _repository.AlterarAsync(marcaCaminhao);
            }
        }

        public async Task ExcluirAsync(MarcaCaminhao entity)
        {
            await _repository.ExcluirAsync(entity);
        }

        /// <summary>
        /// // Não permite inserir uma descrição em duplicidade 
        /// </summary>
        /// <param name="descricao"></param>
        /// <returns></returns>
        private async Task ValidarDescricao(string descricao)
        {
            var existeMarcaCaminhao = await _repository.ExistePorDescricaoAsync(descricao);

            if (existeMarcaCaminhao)
                _notificationContext.AddNotification(new Notification("Descricao",
                        $"A marca de caminhão com a descrição: { descricao } já existe"));
        }
    }
}
