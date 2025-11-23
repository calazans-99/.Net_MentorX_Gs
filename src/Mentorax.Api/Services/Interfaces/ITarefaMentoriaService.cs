using Mentorax.Api.Models.Dto;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mentorax.Api.Services.Interfaces
{
    public interface ITarefaMentoriaService
    {
        Task<(IEnumerable<TarefaMentoriaDto> Items, long TotalCount)> GetPagedAsync(int pageNumber, int pageSize);
        Task<(IEnumerable<TarefaMentoriaDto> Items, long TotalCount)> GetPagedByMentorshipPlanIdAsync(Guid planId, int pageNumber, int pageSize);
        Task<TarefaMentoriaDto?> GetByIdAsync(Guid id);
        Task<TarefaMentoriaDto> CreateAsync(TarefaMentoriaDto dto);
        Task UpdateAsync(Guid id, TarefaMentoriaDto dto);
        Task DeleteAsync(Guid id);
        Task<IEnumerable<TarefaMentoriaDto>> GetByMentorshipPlanIdAsync(Guid planId);
    }
}
