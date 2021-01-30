using JSL.CadCaminhoneiro.Core.Data;
using JSL.CadCaminhoneiro.Domain.Entities;
using System;
using System.Threading.Tasks;

namespace JSL.CadCaminhoneiro.Domain.Interfaces
{
    public interface ICaminhaoRepository : IRepository<Caminhao>
    {
        Task<bool> ExisteAsync(Guid id);
        Task<Caminhao> ObterOriginalAsync(Guid id);
        Task<Caminhao> ObterPorDescricaoAsync(string descricao);
        Task<int> ObterTotalRegistrosAsync(string filter);
        Task<bool> ExistePorDescricaoAsync(string descricao);
    }
}
