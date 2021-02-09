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
        Task<IEnumerable<MotoristaListDto>> ListarTodosSemPaginacaoAsync();
        Task<IEnumerable<EstadoListDto>> ListarEstadosAsync();
        Task<bool> ExisteAsync(Guid id);
        Task<Motorista> ExisteAsync(string cpf, string numeroRegistroGeral, string numeroRegistroHabilitacao);
        Task<ModeloCaminhao> ObterOriginalAsync(Guid id);        
        Task<int> ObterTotalRegistrosAsync(string filter);
        Task<Endereco> ObterEndereco(Guid motoristaId);
        Task<Habilitacao> ObterHabilitacao(Guid motoristaId);
        Task<Caminhao> ObterCaminhao(Guid motoristaId);
        Task<bool> ExistePorCpfAsync(string cpf);
        Task<bool> ExistePorNumeroRegistroGeralAsync(string numeroRegistroGeral);
        Task<bool> ExistePorNumeroRegistroHabilitacaoAsync(string numeroRegistroHabilitacao);
    }
}
