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
    public class TarefaMentoriaService : ITarefaMentoriaService
    {
        private readonly ITarefaMentoriaRepository _repo;
        private readonly IMapper _mapper;
        private readonly ILogger<TarefaMentoriaService> _logger;

        public TarefaMentoriaService(ITarefaMentoriaRepository repo, IMapper mapper, ILogger<TarefaMentoriaService> logger)
        {
            _repo = repo;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<TarefaMentoriaDto> CreateAsync(TarefaMentoriaDto dto)
        {
            var entity = _mapper.Map<TarefaMentoria>(dto);
            await _repo.AddAsync(entity);
            return _mapper.Map<TarefaMentoriaDto>(entity);
        }

        public async Task DeleteAsync(Guid id) => await _repo.DeleteAsync(id);

        public async Task<TarefaMentoriaDto?> GetByIdAsync(Guid id)
        {
            var entity = await _repo.GetByIdAsync(id);
            return entity == null ? null : _mapper.Map<TarefaMentoriaDto>(entity);
        }

        public async Task<(IEnumerable<TarefaMentoriaDto> Items, long TotalCount)> GetPagedAsync(int pageNumber, int pageSize)
        {
            var (items, total) = await _repo.GetPagedAsync(pageNumber, pageSize);
            return (_mapper.Map<IEnumerable<TarefaMentoriaDto>>(items), total);
        }

        public async Task<(IEnumerable<TarefaMentoriaDto> Items, long TotalCount)> GetPagedByMentorshipPlanIdAsync(Guid planId, int pageNumber, int pageSize)
        {
            var (items, total) = await _repo.GetPagedByMentorshipPlanIdAsync(planId, pageNumber, pageSize);
            return (_mapper.Map<IEnumerable<TarefaMentoriaDto>>(items), total);
        }

        public async Task<IEnumerable<TarefaMentoriaDto>> GetByMentorshipPlanIdAsync(Guid planId)
        {
            var items = await _repo.GetByMentorshipPlanIdAsync(planId);
            return _mapper.Map<IEnumerable<TarefaMentoriaDto>>(items);
        }

        public async Task UpdateAsync(Guid id, TarefaMentoriaDto dto)
        {
            var entity = _mapper.Map<TarefaMentoria>(dto);
            entity.Id = id;
            await _repo.UpdateAsync(entity);
        }
    }
}
