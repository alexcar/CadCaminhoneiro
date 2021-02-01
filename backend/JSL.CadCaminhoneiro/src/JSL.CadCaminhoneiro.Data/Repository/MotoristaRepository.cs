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
    public class MotoristaRepository : IMotoristaRepository
    {
        private readonly CadCaminhoneiroContext _context;

        public MotoristaRepository(CadCaminhoneiroContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<MotoristaListDto>> ListarTodosQueryResponseAsync(
            string sort, string filter, int pageNumber, int pageSize)
        {
            if (string.IsNullOrWhiteSpace(sort))
                sort = "nome";

            return await _context.Motorista
                .AsNoTracking()
                .MapMotoristaToDto()
                .ApplyFilter(filter)
                .ApplySort(sort)
                .ApplyPage(pageNumber, pageSize)
                .ToListAsync();
        }

        public async Task IncluirAsync(Motorista entity)
        {
            await _context.Motorista.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task AlterarAsync(Motorista entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }        

        public async Task ExcluirAsync(Motorista entity)
        {
            _context.Motorista.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExisteAsync(Guid id)
        {
            return await _context.Motorista
                .AsNoTracking()
                .AnyAsync(p => p.Id == id);
        }

        public async Task<bool> ExistePorCpfAsync(string cpf)
        {
            return await _context.Motorista
                    .AsNoTracking()
                    .AnyAsync(p => p.Cpf == cpf);
        }

        public Task<bool> ExistePorNumeroRegistroGeralAsync(string numeroRegistroGeral)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> ExistePorNumeroRegistroHabilitacaoAsync(string numeroRegistro)
        {
            return await _context.Motorista
                    .AsNoTracking()
                    .AnyAsync(p => p.Habilitacao.NumeroRegistro == numeroRegistro);
        }        

        public async Task<IEnumerable<Motorista>> ListarTodosAsync(string ordenacao)
        {
            if (string.IsNullOrWhiteSpace(ordenacao))
                ordenacao = "nome";

            return await _context.Motorista
                .AsNoTracking()
                .ApplySort(ordenacao)
                .ToListAsync();
        }        

        public async Task<ModeloCaminhao> ObterOriginalAsync(Guid id)
        {
            return await _context.ModeloCaminhao
                .SingleAsync(p => p.Id == id);
        }

        public async Task<Motorista> ObterPorIdAsync(Guid id)
        {
            return await _context.Motorista
                    .AsNoTracking()
                    .FirstOrDefaultAsync(p => p.Id == id);
        }

        public Task<MotoristaListDto> ObterPorIdQueryResponseAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<int> ObterTotalRegistrosAsync(string filter)
        {
            return await _context.Motorista
                .MapMotoristaToDto()
                .ApplyFilter(filter).CountAsync();
        }

        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}
