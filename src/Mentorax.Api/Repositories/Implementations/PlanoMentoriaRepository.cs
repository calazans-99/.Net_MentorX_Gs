using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Mentorax.Api.Data;
using Mentorax.Api.Models;
using Mentorax.Api.Repositories.Interfaces;
using System.Collections.Generic;
using System;

namespace Mentorax.Api.Repositories.Implementations
{
    public class PlanoMentoriaRepository : IPlanoMentoriaRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<PlanoMentoria> _dbSet;

        public PlanoMentoriaRepository(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<PlanoMentoria>();
        }

        public async Task AddAsync(PlanoMentoria entity)
        {
            _dbSet.Add(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(PlanoMentoria entity)
        {
            _dbSet.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var entity = await GetByIdAsync(id);
            if (entity == null) return;

            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<PlanoMentoria?> GetByIdAsync(Guid id)
        {
            return await _dbSet
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<long> CountAsync()
        {
            return await _dbSet.LongCountAsync();
        }

        // ----------------------------------------------------------------------
        // Implementações corretas exigidas pela interface
        // ----------------------------------------------------------------------

        /// <summary>
        /// Obtém planos por ID de mentorado (sem paginação)
        /// </summary>
        public async Task<IEnumerable<PlanoMentoria>> GetByMenteeIdAsync(Guid mentoradoId)
        {
            return await _dbSet
                .Where(p => p.MentoradoId == mentoradoId)
                .AsNoTracking()
                .ToListAsync();
        }

        /// <summary>
        /// Paginação geral, compatível com a interface
        /// </summary>
        public async Task<(IEnumerable<PlanoMentoria> Items, long TotalCount)> GetPagedAsync(int pageNumber, int pageSize)
        {
            var query = _dbSet.AsQueryable();

            var totalCount = await query.LongCountAsync();

            var items = await query
                .OrderBy(p => p.Title)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (items, totalCount);
        }

        /// <summary>
        /// Paginação filtrando por mentoradoId
        /// </summary>
        public async Task<(IEnumerable<PlanoMentoria> Items, long TotalCount)> GetPagedByMenteeIdAsync(Guid mentoradoId, int pageNumber, int pageSize)
        {
            var query = _dbSet
                .Where(p => p.MentoradoId == mentoradoId);

            var totalCount = await query.LongCountAsync();

            var items = await query
                .OrderBy(p => p.Title)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (items, totalCount);
        }
    }
}
