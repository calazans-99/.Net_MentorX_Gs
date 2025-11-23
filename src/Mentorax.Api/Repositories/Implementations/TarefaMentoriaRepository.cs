using Microsoft.EntityFrameworkCore;
using Mentorax.Api.Data;
using Mentorax.Api.Models;
using Mentorax.Api.Repositories.Interfaces;

namespace Mentorax.Api.Repositories.Implementations
{
    /// <summary>
    /// Implementação do repositório de tarefas de mentoria
    /// </summary>
    public class TarefaMentoriaRepository
        : Repository<TarefaMentoria>, ITarefaMentoriaRepository
    {
        public TarefaMentoriaRepository(ApplicationDbContext context)
            : base(context)
        {
        }

        /// <summary>
        /// Obter todas as tarefas vinculadas a um plano de mentoria específico
        /// </summary>
        public async Task<IEnumerable<TarefaMentoria>> GetByMentorshipPlanIdAsync(Guid planId)
        {
            return await _dbSet
                .Where(t => t.MentorshipPlanId == planId)
                .OrderBy(t => t.Description)
                .ToListAsync();
        }

        /// <summary>
        /// Retorna tarefas paginadas
        /// </summary>
        public async Task<(IEnumerable<TarefaMentoria> Items, long TotalCount)>
            GetPagedAsync(int pageNumber, int pageSize)
        {
            var query = _dbSet.AsQueryable();

            var totalCount = await query.LongCountAsync();

            var items = await query
                .OrderBy(t => t.Description)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (items, totalCount);
        }

        /// <summary>
        /// Lista paginada de tarefas filtrada pelo plano
        /// </summary>
        public async Task<(IEnumerable<TarefaMentoria> Items, long TotalCount)>
            GetPagedByMentorshipPlanIdAsync(Guid planId, int pageNumber, int pageSize)
        {
            var query = _dbSet
                .Where(t => t.MentorshipPlanId == planId)
                .AsQueryable();

            var totalCount = await query.LongCountAsync();

            var items = await query
                .OrderBy(t => t.Description)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (items, totalCount);
        }
    }
}
