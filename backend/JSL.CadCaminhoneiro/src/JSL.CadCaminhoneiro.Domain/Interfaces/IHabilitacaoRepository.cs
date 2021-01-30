using JSL.CadCaminhoneiro.Core.Data;
using JSL.CadCaminhoneiro.Domain.Entities;
using System.Threading.Tasks;

namespace JSL.CadCaminhoneiro.Domain.Interfaces
{
    public interface IHabilitacaoRepository : IRepository<Habilitacao>
    {
        Task<bool> ExisteAsync(string NumeroRegistro);
    }
}
