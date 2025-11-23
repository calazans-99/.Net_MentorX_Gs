using Mentorax.Api.Models;

namespace Mentorax.Api.Repositories.Interfaces
{
    public interface IMentoradoRepository
    {
        Task<Mentorado?> GetByIdAsync(Guid id);
        Task<IEnumerable<Mentorado>> GetAllAsync();
        Task AddAsync(Mentorado entity);
        Task UpdateAsync(Mentorado entity);
        Task DeleteAsync(Guid id);

        Task<(IEnumerable<Mentorado> Items, long TotalCount)> GetPagedAsync(int pageNumber, int pageSize);
        Task<IEnumerable<Mentorado>> GetByDepartmentAsync(string department);
    }
}
