using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JSL.CadCaminhoneiro.Core.Data
{
    public interface IRepository<T> : IDisposable
    {
        Task<IEnumerable<T>> ListarTodosAsync(string ordenacao);
        Task<T> ObterPorIdAsync(Guid id);
        Task IncluirAsync(T entity);
        Task AlterarAsync(T entity);
        Task ExcluirAsync(T entity);
    }
}
