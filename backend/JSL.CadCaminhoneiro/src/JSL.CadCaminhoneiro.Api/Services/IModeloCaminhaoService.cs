using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using JSL.CadCaminhoneiro.Api.Dto;
using JSL.CadCaminhoneiro.Domain.Entities;

namespace JSL.CadCaminhoneiro.Api.Services
{
    public interface IModeloCaminhaoService
    {
        Task<IEnumerable<ModeloCaminhao>> ListarTodosAsync(string ordenacao);
        Task<Guid> IncluirAsync(ModeloCaminhaoIncluirRequest entity);
        Task AlterarAsync(ModeloCaminhaoAlterarRequest entity);
        Task ExcluirAsync(ModeloCaminhao entity);
    }
}
