using Mentorax.Api.Models.Dto;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mentorax.Api.Services.Interfaces
{
    public interface IPlanoMentoriaService
    {
        Task<(IEnumerable<PlanoMentoriaDto> Items, long TotalCount)> GetPagedAsync(int pageNumber, int pageSize);
        Task<(IEnumerable<PlanoMentoriaDto> Items, long TotalCount)> GetPagedByMenteeIdAsync(Guid mentoradoId, int pageNumber, int pageSize);
        Task<PlanoMentoriaDto?> GetByIdAsync(Guid id);
        Task<PlanoMentoriaDto> CreateAsync(PlanoMentoriaDto dto);
        Task UpdateAsync(Guid id, PlanoMentoriaDto dto);
        Task DeleteAsync(Guid id);
        Task<IEnumerable<PlanoMentoriaDto>> GetByMenteeIdAsync(Guid mentoradoId);
    }
}
