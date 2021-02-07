using JSL.CadCaminhoneiro.Core.Data;
using JSL.CadCaminhoneiro.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JSL.CadCaminhoneiro.Domain.Interfaces
{
    public interface IModeloCaminhaoRepository : IRepository<ModeloCaminhao>
    {
        Task<ModeloCaminhaoListDto> ObterPorIdQueryResponseAsync(Guid id);
        Task<IEnumerable<ModeloCaminhaoListDto>> ListarTodosQueryResponseAsync(string sort, string filter, int pageNumber, int pageSize);
        Task<IEnumerable<ModeloCaminhaoListDto>> ListarTodosSemPaginacaoAsync();
        Task<bool> ExisteAsync(Guid id);
        Task<ModeloCaminhao> ObterOriginalAsync(Guid id);
        Task<ModeloCaminhao> ObterPorDescricaoAsync(string descricao);
        Task<ModeloCaminhao> ObterPorDescricaoAnoAsync(string descricao, string ano);
        Task<int> ObterTotalRegistrosAsync(string filter);
        Task<bool> ExistePorDescricaoAsync(string descricao);
        Task<bool> ContemCaminhao(Guid id);
    }
}
