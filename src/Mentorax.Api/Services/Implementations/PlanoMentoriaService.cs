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
    public class PlanoMentoriaService : IPlanoMentoriaService
    {
        private readonly IPlanoMentoriaRepository _repo;
        private readonly IMapper _mapper;
        private readonly ILogger<PlanoMentoriaService> _logger;

        public PlanoMentoriaService(IPlanoMentoriaRepository repo, IMapper mapper, ILogger<PlanoMentoriaService> logger)
        {
            _repo = repo;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<PlanoMentoriaDto> CreateAsync(PlanoMentoriaDto dto)
        {
            var entity = _mapper.Map<PlanoMentoria>(dto);
            await _repo.AddAsync(entity);
            return _mapper.Map<PlanoMentoriaDto>(entity);
        }

        public async Task DeleteAsync(Guid id) => await _repo.DeleteAsync(id);

        public async Task<PlanoMentoriaDto?> GetByIdAsync(Guid id)
        {
            var entity = await _repo.GetByIdAsync(id);
            return entity == null ? null : _mapper.Map<PlanoMentoriaDto>(entity);
        }

        public async Task<(IEnumerable<PlanoMentoriaDto> Items, long TotalCount)> GetPagedAsync(int pageNumber, int pageSize)
        {
            var (items, total) = await _repo.GetPagedAsync(pageNumber, pageSize);
            return (_mapper.Map<IEnumerable<PlanoMentoriaDto>>(items), total);
        }

        public async Task<(IEnumerable<PlanoMentoriaDto> Items, long TotalCount)> GetPagedByMenteeIdAsync(Guid mentoradoId, int pageNumber, int pageSize)
        {
            var (items, total) = await _repo.GetPagedByMenteeIdAsync(mentoradoId, pageNumber, pageSize);
            return (_mapper.Map<IEnumerable<PlanoMentoriaDto>>(items), total);
        }

        public async Task<IEnumerable<PlanoMentoriaDto>> GetByMenteeIdAsync(Guid mentoradoId)
        {
            var items = await _repo.GetByMenteeIdAsync(mentoradoId);
            return _mapper.Map<IEnumerable<PlanoMentoriaDto>>(items);
        }

        public async Task UpdateAsync(Guid id, PlanoMentoriaDto dto)
        {
            var entity = _mapper.Map<PlanoMentoria>(dto);
            entity.Id = id;
            await _repo.UpdateAsync(entity);
        }
    }
}
