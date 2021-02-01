using JSL.CadCaminhoneiro.Api.Dto;
using JSL.CadCaminhoneiro.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JSL.CadCaminhoneiro.Api.Services
{
    public interface IMotoristaService
    {
        Task<IEnumerable<Motorista>> ListarTodosAsync(string ordenacao);
        //Task<Motorista> ObterPorIdAsync(Guid id);
        //Task<bool> BancoExisteAsync(Guid id);
        //Task<Motorista> ObterPorCodigoAsync(string codigo);
        //Task<Motorista> ObterPorNomeAsync(string nome);
        Task<Guid> IncluirAsync(MotoristaIncluirRequest entity);
        Task AlterarAsync(MotoristaAlterarRequest entity);
        Task ExcluirAsync(Motorista entity);
    }
}
