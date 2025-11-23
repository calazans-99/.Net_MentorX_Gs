using Mentorax.Api.Services.Interfaces;
using Mentorax.Api.Repositories.Interfaces;
using Mentorax.Api.Models.Dto;
using AutoMapper;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Mentorax.Api.Models;

namespace Mentorax.Api.Services.Implementations
{
    public class QuestionarioService : IQuestionarioService
    {
        private readonly IQuestionarioRepository _repo;
        private readonly IMapper _mapper;
        private readonly ILogger<QuestionarioService> _logger;

        public QuestionarioService(IQuestionarioRepository repo, IMapper mapper, ILogger<QuestionarioService> logger)
        {
            _repo = repo;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<QuestionarioDto> CreateAsync(QuestionarioDto dto)
        {
            var entity = _mapper.Map<Questionario>(dto);
            await _repo.AddAsync(entity);
            return _mapper.Map<QuestionarioDto>(entity);
        }

        public async Task DeleteAsync(Guid id) => await _repo.DeleteAsync(id);

        public async Task<QuestionarioDto?> GetByIdAsync(Guid id)
        {
            var entity = await _repo.GetByIdAsync(id);
            return entity == null ? null : _mapper.Map<QuestionarioDto>(entity);
        }

        public async Task<(IEnumerable<QuestionarioDto> Items, long TotalCount)> GetPagedAsync(int pageNumber, int pageSize)
        {
            var (items, total) = await _repo.GetPagedAsync(pageNumber, pageSize);
            return (_mapper.Map<IEnumerable<QuestionarioDto>>(items), total);
        }

        public async Task<(IEnumerable<QuestionarioDto> Items, long TotalCount)> GetPagedByMenteeIdAsync(Guid mentoradoId, int pageNumber, int pageSize)
        {
            var (items, total) = await _repo.GetPagedByMenteeIdAsync(mentoradoId, pageNumber, pageSize);
            return (_mapper.Map<IEnumerable<QuestionarioDto>>(items), total);
        }

        public async Task<IEnumerable<QuestionarioDto>> GetByMenteeIdAsync(Guid mentoradoId)
        {
            var items = await _repo.GetByMenteeIdAsync(mentoradoId);
            return _mapper.Map<IEnumerable<QuestionarioDto>>(items);
        }

        public async Task<bool> ExistsForMenteeAsync(Guid mentoradoId)
            => await _repo.ExistsForMenteeAsync(mentoradoId);

        public async Task UpdateAsync(Guid id, QuestionarioDto dto)
        {
            var entity = _mapper.Map<Questionario>(dto);
            entity.Id = id;
            await _repo.UpdateAsync(entity);
        }
    }
}
