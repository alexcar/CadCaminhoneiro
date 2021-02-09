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

        public async Task<IEnumerable<MotoristaListDto>> ListarTodosSemPaginacaoAsync()
        {
            return await _context.Motorista
                .AsNoTracking()
                .MapMotoristaToDto()
                .ApplySort("nome")
                .ToListAsync();
        }

        public async Task<IEnumerable<EstadoListDto>> ListarEstadosAsync()
        {
            return await _context.Estado
                .AsNoTracking()
                .MapEstadoToDto()
                .ToListAsync();
        }

        public async Task<bool> ExisteAsync(Guid id)
        {
            return await _context.Motorista
                .AsNoTracking()
                .AnyAsync(p => p.Id == id);
        }

        public async Task<Motorista> ExisteAsync(
            string cpf, 
            string numeroRegistroGeral, 
            string numeroRegistroHabilitacao)
        {
            return await _context.Motorista
                .AsNoTracking()
                .FirstOrDefaultAsync(p => 
                    p.Cpf == cpf || 
                    p.NumeroRegistroGeral == numeroRegistroGeral || 
                    p.Habilitacao.NumeroRegistro == numeroRegistroHabilitacao);
        }

        public async Task<bool> ExistePorCpfAsync(string cpf)
        {
            return await _context.Motorista
                    .AsNoTracking()
                    .AnyAsync(p => p.Cpf == cpf);
        }

        public async Task<bool> ExistePorNumeroRegistroGeralAsync(string numeroRegistroGeral)
        {
            return await _context.Motorista
                    .AsNoTracking()
                    .AnyAsync(p => p.NumeroRegistroGeral == numeroRegistroGeral);
        }

        public async Task<bool> ExistePorNumeroRegistroHabilitacaoAsync(string numeroRegistroHabilitacao)
        {
            return await _context.Habilitacao
                    .AsNoTracking()
                    .AnyAsync(p => p.NumeroRegistro == numeroRegistroHabilitacao);
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
                    .Include(p => p.Endereco)
                    .Include(p => p.Habilitacao)
                    .Include(p => p.Caminhao)
                    .AsNoTracking()
                    .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<MotoristaListDto> ObterPorIdQueryResponseAsync(Guid id)
        {
            return await _context.Motorista
                .AsNoTracking()
                .MapMotoristaToDto()
                .SingleAsync(p => p.Id == id);
        }

        public async Task<int> ObterTotalRegistrosAsync(string filter)
        {
            return await _context.Motorista
                .MapMotoristaToDto()
                .ApplyFilter(filter).CountAsync();
        }

        public async Task<Endereco> ObterEndereco(Guid motoristaId)
        {
            return await _context.Endereco
                .AsNoTracking()
                .SingleAsync(p => p.MotoristaId == motoristaId);
        }

        public async Task<Habilitacao> ObterHabilitacao(Guid motoristaId)
        {
            return await _context.Habilitacao
                .AsNoTracking()
                .SingleAsync(p => p.MotoristaId == motoristaId);
        }

        public async Task<Caminhao> ObterCaminhao(Guid motoristaId)
        {
            return await _context.Caminhao
                .AsNoTracking()
                .SingleAsync(p => p.MotoristaId == motoristaId);
        }

        public async Task IncluirAsync(Motorista entity)
        {
            await _context.Motorista.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task AlterarAsync(Motorista entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            _context.Entry(entity.Endereco).State = EntityState.Modified;
            _context.Entry(entity.Habilitacao).State = EntityState.Modified;
            _context.Entry(entity.Caminhao).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }        

        public async Task ExcluirAsync(Motorista entity)
        {
            _context.Entry(entity.Caminhao).State = EntityState.Deleted;
            _context.Entry(entity.Endereco).State = EntityState.Deleted;
            _context.Entry(entity.Habilitacao).State = EntityState.Deleted;
            _context.Motorista.Remove(entity);
            await _context.SaveChangesAsync();
        }       

        public void Dispose()
        {
            _context?.Dispose();
        }        
    }
}
