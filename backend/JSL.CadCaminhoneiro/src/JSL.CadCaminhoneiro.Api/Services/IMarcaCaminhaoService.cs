using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using JSL.CadCaminhoneiro.Api.Dto;
using JSL.CadCaminhoneiro.Domain.Entities;

namespace JSL.CadCaminhoneiro.Api.Services
{
    public interface IMarcaCaminhaoService
    {
        Task<IEnumerable<MarcaCaminhao>> ListarTodosAsync(string ordenacao);
        Task<Guid> IncluirAsync(MarcaCaminhaoIncluirRequest entity);
        Task AlterarAsync(MarcaCaminhaoAlterarRequest entity);
        Task ExcluirAsync(MarcaCaminhao entity);
    }
}
