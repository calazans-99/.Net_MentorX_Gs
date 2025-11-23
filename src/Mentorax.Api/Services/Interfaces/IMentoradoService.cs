using Mentorax.Api.Models.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace Mentorax.Api.Services.Interfaces
{
    public interface IMentoradoService
    {
        Task<(IEnumerable<MentoradoDto> Items, long TotalCount)> GetPagedAsync(int pageNumber, int pageSize);
        Task<MentoradoDto?> GetByIdAsync(Guid id);
        Task<MentoradoDto> CreateAsync(MentoradoDto dto);
        Task UpdateAsync(Guid id, MentoradoDto dto);
        Task DeleteAsync(Guid id);
        Task<IEnumerable<MentoradoDto>> GetByDepartmentAsync(string department);
    }
}
