using Mentorax.Api.Models;

namespace Mentorax.Api.Repositories.Interfaces
{
    /// <summary>
    /// Interface específica para repositório de planos de mentoria.
    /// Segue o mesmo padrão da arquitetura do professor, sem repositório genérico.
    /// </summary>
    public interface IPlanoMentoriaRepository
    {
        // Métodos CRUD básicos (obrigatórios em todos os repositórios)
        Task<PlanoMentoria?> GetByIdAsync(Guid id);
        Task<IEnumerable<PlanoMentoria>> GetAllAsync();
        Task AddAsync(PlanoMentoria entity);
        Task UpdateAsync(PlanoMentoria entity);
        Task DeleteAsync(Guid id);

        // Métodos específicos do domínio

        /// <summary>
        /// Obtém planos de mentoria por ID do mentorado.
        /// </summary>
        Task<IEnumerable<PlanoMentoria>> GetByMenteeIdAsync(Guid mentoradoId);

        /// <summary>
        /// Obtém planos com paginação.
        /// </summary>
        Task<(IEnumerable<PlanoMentoria> Items, long TotalCount)>
            GetPagedAsync(int pageNumber, int pageSize);

        /// <summary>
        /// Obtém planos por ID de mentorado com paginação.
        /// </summary>
        Task<(IEnumerable<PlanoMentoria> Items, long TotalCount)>
            GetPagedByMenteeIdAsync(Guid mentoradoId, int pageNumber, int pageSize);
    }
}
