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
    public class MarcaCaminhaoRepository : IMarcaCaminhaoRepository
    {
        private readonly CadCaminhoneiroContext _context;

        public MarcaCaminhaoRepository(CadCaminhoneiroContext context)
        {
            _context = context;
        }        

        public async Task<bool> ExisteAsync(Guid id)
        {
            return await _context.MarcaCaminhao
                .AsNoTracking()
                .AnyAsync(p => p.Id == id);
        }

        public async Task<bool> ExistePorDescricaoAsync(string descricao)
        {
            return await _context.MarcaCaminhao
                .AsNoTracking()
                .AnyAsync(p => p.Descricao == descricao);
        }        

        public async Task<IEnumerable<MarcaCaminhao>> ListarTodosAsync(string ordenacao)
        {
            if (string.IsNullOrWhiteSpace(ordenacao))
                ordenacao = "codigo";

            return await _context.MarcaCaminhao
                .AsNoTracking()
                .ApplySort(ordenacao)
                .ToListAsync();
        }

        public async Task<IEnumerable<MarcaCaminhaoListDto>> ListarTodosQueryResponseAsync(
            string sort, string filter, int pageNumber, int pageSize)
        {
            if (string.IsNullOrWhiteSpace(sort))
                sort = "descricao";

            return await _context.MarcaCaminhao
                .AsNoTracking()
                .MapMarcaCaminhaoToDto()
                .ApplyFilter(filter)
                .ApplySort(sort)
                .ApplyPage(pageNumber, pageSize)                
                .ToListAsync();
        }

        public async Task<MarcaCaminhao> ObterOriginalAsync(Guid id)
        {
            return await _context.MarcaCaminhao
                .AsNoTracking()
                .SingleAsync(p => p.Id == id);
        }

        public async Task<MarcaCaminhao> ObterPorDescricaoAsync(string descricao)
        {
            return await _context.MarcaCaminhao
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Descricao == descricao);
        }

        public async Task<MarcaCaminhao> ObterPorIdAsync(Guid id)
        {
            return await _context.MarcaCaminhao
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<MarcaCaminhaoListDto> ObterPorIdQueryResponseAsync(Guid id)
        {
            return await _context.MarcaCaminhao
                    .MapMarcaCaminhaoToDto()
                    .AsNoTracking()
                    .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<int> ObterTotalRegistrosAsync(string filter)
        {
            return await _context.MarcaCaminhao
                .MapMarcaCaminhaoToDto()
                .ApplyFilter(filter).CountAsync();
        }

        public async Task IncluirAsync(MarcaCaminhao entity)
        {
            await _context.MarcaCaminhao.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task AlterarAsync(MarcaCaminhao entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task ExcluirAsync(MarcaCaminhao entity)
        {
            _context.MarcaCaminhao.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}
