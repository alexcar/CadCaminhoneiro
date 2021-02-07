using JSL.CadCaminhoneiro.Core.Data;
using JSL.CadCaminhoneiro.Domain.Entities;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace JSL.CadCaminhoneiro.Domain.Interfaces
{
    public interface IMarcaCaminhaoRepository : IRepository<MarcaCaminhao>
    {
        Task<MarcaCaminhaoListDto> ObterPorIdQueryResponseAsync(Guid id);
        Task<IEnumerable<MarcaCaminhaoListDto>> ListarTodosQueryResponseAsync(string sort, string filter, int pageNumber, int pageSize);
        Task<IEnumerable<MarcaCaminhaoListDto>> ListarTodosSemPaginacaoAsync();
        Task<bool> ExisteAsync(Guid id);
        Task<MarcaCaminhao> ObterOriginalAsync(Guid id);
        Task<MarcaCaminhao> ObterPorDescricaoAsync(string descricao);
        Task<int> ObterTotalRegistrosAsync(string filter);
        Task<bool> ExistePorDescricaoAsync(string descricao);
        Task<bool> ContemModeloCaminhao(Guid id);
    }
}
