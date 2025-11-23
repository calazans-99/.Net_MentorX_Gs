using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Mentorax.Api.Data;
using Mentorax.Api.Models;
using Mentorax.Api.Repositories.Interfaces;
using System.Collections.Generic;

namespace Mentorax.Api.Repositories.Implementations
{
    public class MentoradoRepository : IMentoradoRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<Mentorado> _dbSet;

        public MentoradoRepository(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<Mentorado>();
        }

        public async Task<Mentorado?> GetByIdAsync(Guid id)
        {
            return await _dbSet
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task AddAsync(Mentorado entity)
        {
            _dbSet.Add(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Mentorado entity)
        {
            _dbSet.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var entity = await _dbSet.FirstOrDefaultAsync(m => m.Id == id);
            if (entity == null) return;

            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<long> CountAsync()
        {
            return await _dbSet.LongCountAsync();
        }

        /// <summary>
        /// Implementação compatível com a INTERFACE
        /// </summary>
        public async Task<(IEnumerable<Mentorado> Items, long TotalCount)> GetPagedAsync(int pageNumber, int pageSize)
        {
            var query = _dbSet.AsQueryable();

            var totalCount = await query.LongCountAsync();

            var items = await query
                .OrderBy(m => m.FullName)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (items, totalCount);
        }

        /// <summary>
        /// Método faltando – agora implementado
        /// </summary>
        public async Task<IEnumerable<Mentorado>> GetByDepartmentAsync(string department)
        {
            return await _dbSet
                .Where(m => m.Department == department)
                .AsNoTracking()
                .ToListAsync();
        }

        public Task<IEnumerable<Mentorado>> GetAllAsync()
        {
            throw new NotImplementedException();
        }
    }
}
