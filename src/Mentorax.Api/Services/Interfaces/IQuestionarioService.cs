using Mentorax.Api.Models.Dto;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mentorax.Api.Services.Interfaces
{
    public interface IQuestionarioService
    {
        Task<(IEnumerable<QuestionarioDto> Items, long TotalCount)> GetPagedAsync(int pageNumber, int pageSize);
        Task<(IEnumerable<QuestionarioDto> Items, long TotalCount)> GetPagedByMenteeIdAsync(Guid mentoradoId, int pageNumber, int pageSize);
        Task<QuestionarioDto?> GetByIdAsync(Guid id);
        Task<QuestionarioDto> CreateAsync(QuestionarioDto dto);
        Task UpdateAsync(Guid id, QuestionarioDto dto);
        Task DeleteAsync(Guid id);
        Task<IEnumerable<QuestionarioDto>> GetByMenteeIdAsync(Guid mentoradoId);
        Task<bool> ExistsForMenteeAsync(Guid mentoradoId);
    }
}
