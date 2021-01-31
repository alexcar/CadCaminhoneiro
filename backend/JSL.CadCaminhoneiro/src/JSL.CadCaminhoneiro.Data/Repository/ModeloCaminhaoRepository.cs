using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using JSL.CadCaminhoneiro.Domain.Entities;
using JSL.CadCaminhoneiro.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace JSL.CadCaminhoneiro.Data.Repository
{
    public class ModeloCaminhaoRepository : IModeloCaminhaoRepository
    {
        private readonly CadCaminhoneiroContext _context;

        public ModeloCaminhaoRepository(CadCaminhoneiroContext context)
        {
            _context = context;
        }

        public async Task AlterarAsync(ModeloCaminhao entity)
        {
            await _context.SaveChangesAsync();
        }        

        public async Task ExcluirAsync(ModeloCaminhao entity)
        {
            _context.ModeloCaminhao.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExisteAsync(Guid id)
        {
            return await _context.ModeloCaminhao
                .AsNoTracking()
                .AnyAsync(p => p.Id == id);
        }

        public async Task<bool> ExistePorDescricaoAsync(string descricao)
        {
            return await _context.ModeloCaminhao
                .AsNoTracking()
                .AnyAsync(p => p.Descricao == descricao);
        }

        public async Task IncluirAsync(ModeloCaminhao entity)
        {
            await _context.ModeloCaminhao.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<ModeloCaminhao>> ListarTodosAsync(string ordenacao)
        {
            if (string.IsNullOrWhiteSpace(ordenacao))
                ordenacao = "codigo";

            return await _context.ModeloCaminhao
                .AsNoTracking()
                .ApplySort(ordenacao)
                .ToListAsync();
        }

        public Task<IEnumerable<ModeloCaminhaoListDto>> ListarTodosQueryResponseAsync(string sort, string filter, int pageNumber, int pageSize)
        {
            throw new NotImplementedException();
        }

        public async Task<ModeloCaminhao> ObterOriginalAsync(Guid id)
        {
            return await _context.ModeloCaminhao
                .AsNoTracking()
                .SingleAsync(p => p.Id == id);
        }

        public async Task<ModeloCaminhao> ObterPorDescricaoAsync(string descricao)
        {
            return await _context.ModeloCaminhao
                .AsNoTracking()
                .SingleAsync(p => p.Descricao == descricao);
        }

        public async Task<ModeloCaminhao> ObterPorIdAsync(Guid id)
        {
            return await _context.ModeloCaminhao
                .AsNoTracking()
                .SingleAsync(p => p.Id == id);
        }

        public Task<ModeloCaminhaoListDto> ObterPorIdQueryResponseAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<int> ObterTotalRegistrosAsync(string filter)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}
