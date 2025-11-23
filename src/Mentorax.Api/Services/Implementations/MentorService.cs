using Mentorax.Api.Services.Interfaces;
using Mentorax.Api.Repositories.Interfaces;
using Mentorax.Api.Models.Dto;
using AutoMapper;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mentorax.Api.Services.Implementations
{
    public class MentorService : IMentorService
    {
        private readonly IMentorRepository _repo;
        private readonly IMapper _mapper;
        private readonly ILogger<MentorService> _logger;

        public MentorService(IMentorRepository repo, IMapper mapper, ILogger<MentorService> logger)
        {
            _repo = repo;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<MentorDto> CreateAsync(MentorDto dto)
        {
            var entity = _mapper.Map<Mentorax.Api.Models.Mentor>(dto);
            await _repo.AddAsync(entity);
            return _mapper.Map<MentorDto>(entity);
        }

        public async Task DeleteAsync(Guid id) => await _repo.DeleteAsync(id);

        public async Task<MentorDto?> GetByIdAsync(Guid id)
        {
            var entity = await _repo.GetByIdAsync(id);
            return entity == null ? null : _mapper.Map<MentorDto>(entity);
        }

        public async Task<(IEnumerable<MentorDto> Items, long TotalCount)> GetPagedAsync(int pageNumber, int pageSize)
        {
            var (items, total) = await _repo.GetPagedAsync(pageNumber, pageSize);
            return (_mapper.Map<IEnumerable<MentorDto>>(items), total);
        }

        public async Task<bool> EmailExistsAsync(string email, Guid? excludeId = null)
            => await _repo.EmailExistsAsync(email, excludeId);

        public async Task<MentorDto?> GetByEmailAsync(string email)
        {
            var entity = await _repo.GetByEmailAsync(email);
            return entity == null ? null : _mapper.Map<MentorDto>(entity);
        }

        public async Task UpdateAsync(Guid id, MentorDto dto)
        {
            var entity = _mapper.Map<Mentorax.Api.Models.Mentor>(dto);
            entity.Id = id;
            await _repo.UpdateAsync(entity);
        }
    }
}
