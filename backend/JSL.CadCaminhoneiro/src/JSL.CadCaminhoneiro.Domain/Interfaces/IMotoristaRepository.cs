using JSL.CadCaminhoneiro.Core.Data;
using JSL.CadCaminhoneiro.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JSL.CadCaminhoneiro.Domain.Interfaces
{
    public interface IMotoristaRepository : IRepository<Motorista>
    {
        Task<MotoristaListDto> ObterPorIdQueryResponseAsync(Guid id);
        Task<IEnumerable<MotoristaListDto>> ListarTodosQueryResponseAsync(string sort, string filter, int pageNumber, int pageSize);
        Task<bool> ExisteAsync(Guid id);
        Task<ModeloCaminhao> ObterOriginalAsync(Guid id);        
        Task<int> ObterTotalRegistrosAsync(string filter);
        Task<bool> ExistePorCpfAsync(string cpf);
        Task<bool> ExistePorNumeroRegistroGeralAsync(string numeroRegistroGeral);
        Task<bool> ExistePorNumeroRegistroHabilitacaoAsync(string numeroRegistro);
    }
}
