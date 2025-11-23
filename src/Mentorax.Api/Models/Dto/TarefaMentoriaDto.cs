using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace Mentorax.Api.Models.Dto
{
    /// <summary>
    /// DTO para tarefa de um plano de mentoria.
    /// </summary>
    public class TarefaMentoriaDto
    {
        /// <summary>
        /// Identificador único da tarefa.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Identificador do plano de mentoria ao qual a tarefa pertence.
        /// </summary>
        [Required(ErrorMessage = "O ID do plano de mentoria é obrigatório.")]
        public Guid MentorshipPlanId { get; set; }

        /// <summary>
        /// Descrição da tarefa da mentoria.
        /// </summary>
        [Required(ErrorMessage = "A descrição da tarefa é obrigatória.")]
        [StringLength(1500, ErrorMessage = "A descrição deve ter no máximo 1500 caracteres.")]
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// Quantidade estimada de horas necessárias para concluir a tarefa.
        /// </summary>
        [Required(ErrorMessage = "As horas estimadas são obrigatórias.")]
        [Range(1, 200, ErrorMessage = "As horas estimadas devem estar entre 1 e 200.")]
        public int EstimatedHours { get; set; }

        /// <summary>
        /// Indica se a tarefa já foi concluída.
        /// </summary>
        public bool Done { get; set; }

        /// <summary>
        /// Links HATEOAS relacionados ao recurso.
        /// </summary>
        public List<LinkDto> Links { get; set; } = new();
    }
}
