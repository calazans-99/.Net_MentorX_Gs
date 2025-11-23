using Mentorax.Api.Models;

namespace Mentorax.Api.Repositories.Interfaces
{
    /// <summary>
    /// Interface específica para repositório de questionários,
    /// alinhada ao padrão usado pelo professor (sem IRepository<T>).
    /// </summary>
    public interface IQuestionarioRepository
    {
        // ----------------------
        // MÉTODOS CRUD BÁSICOS
        // ----------------------

        /// <summary>
        /// Obtém um questionário pelo Id.
        /// </summary>
        Task<Questionario?> GetByIdAsync(Guid id);

        /// <summary>
        /// Obtém todos os questionários sem paginação.
        /// </summary>
        Task<IEnumerable<Questionario>> GetAllAsync();

        /// <summary>
        /// Adiciona um novo questionário.
        /// </summary>
        Task AddAsync(Questionario entity);

        /// <summary>
        /// Atualiza um questionário existente.
        /// </summary>
        Task UpdateAsync(Questionario entity);

        /// <summary>
        /// Remove um questionário pelo Id.
        /// </summary>
        Task DeleteAsync(Guid id);

        // ----------------------
        // MÉTODOS ESPECÍFICOS
        // ----------------------

        /// <summary>
        /// Obtém todos os questionários com paginação.
        /// </summary>
        Task<(IEnumerable<Questionario> Items, long TotalCount)>
            GetPagedAsync(int pageNumber, int pageSize);

        /// <summary>
        /// Obtém questionários de um mentorado com paginação.
        /// </summary>
        Task<(IEnumerable<Questionario> Items, long TotalCount)>
            GetPagedByMenteeIdAsync(Guid mentoradoId, int pageNumber, int pageSize);

        /// <summary>
        /// Obtém todos os questionários de um mentorado.
        /// </summary>
        Task<IEnumerable<Questionario>> GetByMenteeIdAsync(Guid mentoradoId);

        /// <summary>
        /// Verifica se existe algum questionário ligado ao mentorado.
        /// </summary>
        Task<bool> ExistsForMenteeAsync(Guid mentoradoId);
    }
}
