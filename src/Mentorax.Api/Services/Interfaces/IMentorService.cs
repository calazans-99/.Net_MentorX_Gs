using Mentorax.Api.Models.Dto;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mentorax.Api.Services.Interfaces
{
    public interface IMentorService
    {
        Task<(IEnumerable<MentorDto> Items, long TotalCount)> GetPagedAsync(int pageNumber, int pageSize);
        Task<MentorDto?> GetByIdAsync(Guid id);
        Task<MentorDto> CreateAsync(MentorDto dto);
        Task UpdateAsync(Guid id, MentorDto dto);
        Task DeleteAsync(Guid id);
        Task<bool> EmailExistsAsync(string email, Guid? excludeId = null);
        Task<MentorDto?> GetByEmailAsync(string email);
    }
}
