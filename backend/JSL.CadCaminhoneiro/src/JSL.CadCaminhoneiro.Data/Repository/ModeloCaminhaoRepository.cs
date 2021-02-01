using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using JSL.CadCaminhoneiro.Data.Repository.Extensions;
using JSL.CadCaminhoneiro.Data.Repository.QueryObjects;
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

        public async Task<IEnumerable<ModeloCaminhaoListDto>> ListarTodosQueryResponseAsync(
            string sort, string filter, int pageNumber, int pageSize)
        {
            if (string.IsNullOrWhiteSpace(sort))
                sort = "descricao";

            return await _context.ModeloCaminhao
                .AsNoTracking()
                .MapModeloCaminhaoToDto()
                .ApplyFilter(filter)
                .ApplySort(sort)
                .ApplyPage(pageNumber, pageSize)
                .ToListAsync();
        }

        public async Task<ModeloCaminhao> ObterOriginalAsync(Guid id)
        {
            return await _context.ModeloCaminhao
                .AsNoTracking()
                .SingleAsync(p => p.Id == id);
        }

        public async Task<ModeloCaminhao> ObterPorDescricaoAsync(string descricao)
        {
            // SingleAsync - se não encontrar, vai gerar uma exceção

            return await _context.ModeloCaminhao
                .AsNoTracking()
                .SingleAsync(p => p.Descricao == descricao);
        }

        public async Task<ModeloCaminhao> ObterPorIdAsync(Guid id)
        {
            return await _context.ModeloCaminhao
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<ModeloCaminhao> ObterPorDescricaoAnoAsync(string descricao, string ano)
        {
            // FirstOrDefaultAsync - se não encontrar, vai retornar null

            return await _context.ModeloCaminhao
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Descricao == descricao && p.Ano == ano);
        }

        public async Task<ModeloCaminhaoListDto> ObterPorIdQueryResponseAsync(Guid id)
        {
            return await _context.ModeloCaminhao
                    .MapModeloCaminhaoToDto()
                    .AsNoTracking()
                    .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<int> ObterTotalRegistrosAsync(string filter)
        {
            return await _context.ModeloCaminhao
                .MapModeloCaminhaoToDto()
                .ApplyFilter(filter).CountAsync();
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

        public async Task<IEnumerable<ModeloCaminhao>> ListarTodosAsync(string ordenacao)
        {
            if (string.IsNullOrWhiteSpace(ordenacao))
                ordenacao = "codigo";

            return await _context.ModeloCaminhao
                .AsNoTracking()
                .ApplySort(ordenacao)
                .ToListAsync();
        }

        public async Task IncluirAsync(ModeloCaminhao entity)
        {
            await _context.ModeloCaminhao.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task AlterarAsync(ModeloCaminhao entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }        

        public async Task ExcluirAsync(ModeloCaminhao entity)
        {
            _context.ModeloCaminhao.Remove(entity);
            await _context.SaveChangesAsync();
        }                

        public void Dispose()
        {
            _context?.Dispose();
        }        
    }
}
