using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Mentorax.Api.Models;

namespace Mentorax.Api.Repositories.Interfaces
{
    /// <summary>
    /// Interface do repositório de tarefas de mentoria,
    /// agora alinhada ao padrão do professor (sem IRepository<T>).
    /// </summary>
    public interface ITarefaMentoriaRepository
    {
        // ----------------------
        // MÉTODOS CRUD BÁSICOS
        // ----------------------

        /// <summary>
        /// Obtém uma tarefa pelo Id.
        /// </summary>
        Task<TarefaMentoria?> GetByIdAsync(Guid id);

        /// <summary>
        /// Obtém todas as tarefas.
        /// </summary>
        Task<IEnumerable<TarefaMentoria>> GetAllAsync();

        /// <summary>
        /// Adiciona uma nova tarefa.
        /// </summary>
        Task AddAsync(TarefaMentoria entity);

        /// <summary>
        /// Atualiza uma tarefa existente.
        /// </summary>
        Task UpdateAsync(TarefaMentoria entity);

        /// <summary>
        /// Remove uma tarefa pelo Id.
        /// </summary>
        Task DeleteAsync(Guid id);

        // ----------------------
        // MÉTODOS ESPECÍFICOS
        // ----------------------

        /// <summary>
        /// Retorna todas as tarefas associadas a um plano de mentoria.
        /// </summary>
        Task<IEnumerable<TarefaMentoria>> GetByMentorshipPlanIdAsync(Guid planId);

        /// <summary>
        /// Retorna lista paginada de tarefas.
        /// </summary>
        Task<(IEnumerable<TarefaMentoria> Items, long TotalCount)>
            GetPagedAsync(int pageNumber, int pageSize);

        /// <summary>
        /// Retorna lista paginada filtrada por plano.
        /// </summary>
        Task<(IEnumerable<TarefaMentoria> Items, long TotalCount)>
            GetPagedByMentorshipPlanIdAsync(Guid planId, int pageNumber, int pageSize);
    }
}
