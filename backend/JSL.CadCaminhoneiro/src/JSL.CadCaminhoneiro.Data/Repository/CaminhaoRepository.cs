using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using JSL.CadCaminhoneiro.Domain.Entities;
using JSL.CadCaminhoneiro.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace JSL.CadCaminhoneiro.Data.Repository
{
    public class CaminhaoRepository : ICaminhaoRepository
    {
        private readonly CadCaminhoneiroContext _context;

        public CaminhaoRepository(CadCaminhoneiroContext context)
        {
            _context = context;
        }
        
        public async Task AlterarAsync(Caminhao entity)
        {
            await _context.SaveChangesAsync();
        }        

        public async Task ExcluirAsync(Caminhao entity)
        {
            _context.Caminhao.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExisteAsync(Guid id)
        {
            return await _context.Caminhao
                .AsNoTracking()
                .AnyAsync(p => p.Id == id);
        }

        public Task<bool> ExistePorDescricaoAsync(string descricao)
        {
            throw new NotImplementedException();
        }

        public async Task IncluirAsync(Caminhao entity)
        {
            await _context.Caminhao.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Caminhao>> ListarTodosAsync(string ordenacao)
        {
            if (string.IsNullOrWhiteSpace(ordenacao))
                ordenacao = "codigo";

            return await _context.Caminhao
                .AsNoTracking()
                .ApplySort(ordenacao)
                .ToListAsync();
        }

        public Task<Caminhao> ObterOriginalAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<Caminhao> ObterPorDescricaoAsync(string descricao)
        {
            throw new NotImplementedException();
        }

        public async Task<Caminhao> ObterPorIdAsync(Guid id)
        {
            return await _context.Caminhao
                    .AsNoTracking()
                    .FirstOrDefaultAsync(p => p.Id == id);
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
