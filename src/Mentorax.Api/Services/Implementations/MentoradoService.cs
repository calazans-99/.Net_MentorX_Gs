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
    public class MentoradoService : IMentoradoService
    {
        private readonly IMentoradoRepository _repo;
        private readonly IMapper _mapper;
        private readonly ILogger<MentoradoService> _logger;

        public MentoradoService(IMentoradoRepository repo, IMapper mapper, ILogger<MentoradoService> logger)
        {
            _repo = repo;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<MentoradoDto> CreateAsync(MentoradoDto dto)
        {
            var entity = _mapper.Map<Mentorax.Api.Models.Mentorado>(dto);
            await _repo.AddAsync(entity);
            return _mapper.Map<MentoradoDto>(entity);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _repo.DeleteAsync(id);
        }

        public async Task<MentoradoDto?> GetByIdAsync(Guid id)
        {
            var entity = await _repo.GetByIdAsync(id);
            return entity == null ? null : _mapper.Map<MentoradoDto>(entity);
        }

        public async Task<(IEnumerable<MentoradoDto> Items, long TotalCount)> GetPagedAsync(int pageNumber, int pageSize)
        {
            var (items, total) = await _repo.GetPagedAsync(pageNumber, pageSize);
            var dtos = _mapper.Map<IEnumerable<MentoradoDto>>(items);
            return (dtos, total);
        }

        public async Task<IEnumerable<MentoradoDto>> GetByDepartmentAsync(string department)
        {
            var items = await _repo.GetByDepartmentAsync(department);
            return _mapper.Map<IEnumerable<MentoradoDto>>(items);
        }

        public async Task UpdateAsync(Guid id, MentoradoDto dto)
        {
            var entity = _mapper.Map<Mentorax.Api.Models.Mentorado>(dto);
            entity.Id = id;
            await _repo.UpdateAsync(entity);
        }
    }
}
