using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoWrapper.Wrappers;
using JSL.CadCaminhoneiro.Api.Infrastructure.Notifications;
using JSL.CadCaminhoneiro.Domain.Entities;
using JSL.CadCaminhoneiro.Domain.Interfaces;
using Microsoft.Extensions.Logging;

namespace JSL.CadCaminhoneiro.Api.Services
{
    public class MotoristaService : IMotoristaService
    {
        private readonly ILogger<MotoristaService> _logger;
        private readonly IMotoristaRepository _repository;
        private readonly NotificationContext _notificationContext;

        // TODO: Criar exceção e verificar se o log vai registrar o erro.

        public MotoristaService(
            ILogger<MotoristaService> logger,
            IMotoristaRepository repository,
            NotificationContext notificationContext)
        {
            _logger = logger;
            _repository = repository;
            _notificationContext = notificationContext;
        }

        public async Task<IEnumerable<Motorista>> ListarTodosAsync(string ordenacao)
        {
            return await _repository.ListarTodosAsync(ordenacao);
        }

        public Task<Guid> IncluirAsync(Motorista entity)
        {
            throw new NotImplementedException();
        }

        public Task AlterarAsync(Motorista entity)
        {
            throw new NotImplementedException();
        }

        public async Task ExcluirAsync(Motorista entity)
        {
            await _repository.ExcluirAsync(entity);
        }        
    }
}
