using Microsoft.AspNetCore.Mvc;
using Mentorax.Api.Repositories.Interfaces;
using Mentorax.Api.Models.Dto;
using AutoMapper;
using Microsoft.Extensions.Logging;

namespace Mentorax.Api.Controllers
{
    /// <summary>
    /// Controller para gerenciamento de mentores.
    /// </summary>
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Produces("application/json")]
    public class MentoresController : ControllerBase
    {
        private readonly IMentorRepository _repo;
        private readonly IMapper _mapper;
        private readonly ILogger<MentoresController> _logger;

        public MentoresController(
            IMentorRepository repo,
            IMapper mapper,
            ILogger<MentoresController> logger)
        {
            _repo = repo;
            _mapper = mapper;
            _logger = logger;
        }

        /// <summary>
        /// Lista todos os mentores com paginação.
        /// </summary>
        /// <param name="page">Número da página (>= 1)</param>
        /// <param name="pageSize">Tamanho da página (1-100)</param>
        /// <returns>Lista paginada de mentores.</returns>
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

                var dtos = _mapper.Map<IEnumerable<MentorDto>>(paged.Items);

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
                _logger.LogError(ex, "Erro ao listar mentores");
                return StatusCode(500, "Erro interno do servidor");
            }
        }

        /// <summary>
        /// Obtém um mentor pelo ID.
        /// </summary>
        /// <param name="id">ID do mentor.</param>
        /// <returns>Mentor encontrado.</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Resource<MentorDto>), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> ObterPorId(Guid id)
        {
            try
            {
                var entity = await _repo.GetByIdAsync(id);
                if (entity == null)
                    return NotFound($"Mentor com ID {id} não encontrado");

                var dto = _mapper.Map<MentorDto>(entity);

                var resource = new Resource<MentorDto>(dto);
                resource.Links.Add(new Link(Url.Action(nameof(ObterPorId), new { id }), "self", "GET"));

                return Ok(resource);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao obter mentor com ID {Id}", id);
                return StatusCode(500, "Erro interno do servidor");
            }
        }

        /// <summary>
        /// Cria um novo mentor.
        /// </summary>
        /// <param name="model">Dados do mentor.</param>
        /// <returns>Mentor criado.</returns>
        [HttpPost]
        [ProducesResponseType(typeof(MentorDto), 201)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Criar([FromBody] MentorDto model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var entity = _mapper.Map<Mentorax.Api.Models.Mentor>(model);

                await _repo.AddAsync(entity);

                var dto = _mapper.Map<MentorDto>(entity);

                return CreatedAtAction(nameof(ObterPorId), new
                {
                    id = entity.Id
                }, dto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao criar mentor");
                return StatusCode(500, "Erro interno do servidor");
            }
        }

        /// <summary>
        /// Atualiza um mentor existente.
        /// </summary>
        /// <param name="id">ID do mentor.</param>
        /// <param name="model">Dados atualizados.</param>
        /// <returns>Mentor atualizado.</returns>
        [HttpPut("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Atualizar(Guid id, [FromBody] MentorDto model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var existente = await _repo.GetByIdAsync(id);
                if (existente == null)
                    return NotFound($"Mentor com ID {id} não encontrado");

                var entity = _mapper.Map<Mentorax.Api.Models.Mentor>(model);
                entity.Id = id;

                await _repo.UpdateAsync(entity);

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao atualizar mentor com ID {Id}", id);
                return StatusCode(500, "Erro interno do servidor");
            }
        }

        /// <summary>
        /// Exclui um mentor.
        /// </summary>
        /// <param name="id">ID do mentor.</param>
        /// <returns>Resultado da operação.</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Excluir(Guid id)
        {
            try
            {
                var existente = await _repo.GetByIdAsync(id);
                if (existente == null)
                    return NotFound($"Mentor com ID {id} não encontrado");

                await _repo.DeleteAsync(id);

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao excluir mentor com ID {Id}", id);
                return StatusCode(500, "Erro interno do servidor");
            }
        }
    }
}
