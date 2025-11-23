using Microsoft.AspNetCore.Mvc;
using Mentorax.Api.Repositories.Interfaces;
using Mentorax.Api.Models.Dto;
using AutoMapper;
using Microsoft.Extensions.Logging;
using Mentorax.Api.Repositories.Implementations;

namespace Mentorax.Api.Controllers
{
    /// <summary>
    /// Controller para gerenciamento de planos de mentoria.
    /// </summary>
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Produces("application/json")]
    public class PlanosMentoriaController : ControllerBase
    {
        private readonly IPlanoMentoriaRepository _repo;
        private readonly IMapper _mapper;
        private readonly ILogger<PlanosMentoriaController> _logger;

        public PlanosMentoriaController(
            IPlanoMentoriaRepository repo,
            IMapper mapper,
            ILogger<PlanosMentoriaController> logger)
        {
            _repo = repo;
            _mapper = mapper;
            _logger = logger;
        }

        /// <summary>
        /// Lista todos os planos de mentoria com paginação.
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(Resource<object>), 200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> ObterTodos(
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 10)
        {
            try
            {
                if (page < 1 || pageSize < 1 || pageSize > 100)
                    return BadRequest("page deve ser >= 1 e pageSize deve estar entre 1 e 100");

                var paged = await _repo.GetAllAsync(page, pageSize);

                var dtos = _mapper.Map<IEnumerable<PlanoMentoriaDto>>(paged.Items);

                var resource = new Resource<object>(new
                {
                    Items = dtos,
                    paged.Page,
                    paged.PageSize,
                    paged.TotalCount
                });

                var self = Url.Action(nameof(ObterTodos), new { page, pageSize });
                resource.Links.Add(new Link(self, "self", "GET"));

                if (paged.Page < paged.TotalPages)
                {
                    var next = Url.Action(nameof(ObterTodos), new { page = paged.Page + 1, pageSize });
                    resource.Links.Add(new Link(next, "next", "GET"));
                }

                return Ok(resource);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao listar planos de mentoria");
                return StatusCode(500, "Erro interno do servidor");
            }
        }

        /// <summary>
        /// Obtém um plano de mentoria pelo ID.
        /// </summary>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Resource<PlanoMentoriaDto>), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> ObterPorId(Guid id)
        {
            try
            {
                var entity = await _repo.GetByIdAsync(id);
                if (entity == null)
                    return NotFound($"Plano de mentoria com ID {id} não encontrado");

                var dto = _mapper.Map<PlanoMentoriaDto>(entity);

                var resource = new Resource<PlanoMentoriaDto>(dto);
                resource.Links.Add(new Link(Url.Action(nameof(ObterPorId), new { id }), "self", "GET"));

                return Ok(resource);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao obter plano de mentoria com ID {Id}", id);
                return StatusCode(500, "Erro interno do servidor");
            }
        }

        /// <summary>
        /// Cria um novo plano de mentoria.
        /// </summary>
        [HttpPost]
        [ProducesResponseType(typeof(PlanoMentoriaDto), 201)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Criar([FromBody] PlanoMentoriaDto model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var entity = _mapper.Map<Mentorax.Api.Models.PlanoMentoria>(model);

                await _repo.AddAsync(entity);

                var dto = _mapper.Map<PlanoMentoriaDto>(entity);

                return CreatedAtAction(nameof(ObterPorId), new { id = entity.Id }, dto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao criar plano de mentoria");
                return StatusCode(500, "Erro interno do servidor");
            }
        }

        /// <summary>
        /// Atualiza um plano de mentoria existente.
        /// </summary>
        [HttpPut("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Atualizar(Guid id, [FromBody] PlanoMentoriaDto model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var existente = await _repo.GetByIdAsync(id);
                if (existente == null)
                    return NotFound($"Plano de mentoria com ID {id} não encontrado");

                var entity = _mapper.Map<Mentorax.Api.Models.PlanoMentoria>(model);
                entity.Id = id;

                await _repo.UpdateAsync(entity);

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao atualizar plano de mentoria com ID {Id}", id);
                return StatusCode(500, "Erro interno do servidor");
            }
        }

        /// <summary>
        /// Exclui um plano de mentoria.
        /// </summary>
        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Excluir(Guid id)
        {
            try
            {
                var existente = await _repo.GetByIdAsync(id);
                if (existente == null)
                    return NotFound($"Plano de mentoria com ID {id} não encontrado");

                await _repo.DeleteAsync(id);

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao excluir plano de mentoria com ID {Id}", id);
                return StatusCode(500, "Erro interno do servidor");
            }
        }
    }
}
