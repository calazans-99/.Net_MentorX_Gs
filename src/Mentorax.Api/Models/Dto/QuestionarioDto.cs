using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace Mentorax.Api.Models.Dto
{
    /// <summary>
    /// DTO para resposta do questionário do mentorado.
    /// </summary>
    public class QuestionarioDto
    {
        /// <summary>
        /// Identificador único do questionário.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Identificador do mentorado que respondeu o questionário.
        /// </summary>
        [Required(ErrorMessage = "O ID do mentorado é obrigatório.")]
        public Guid MenteeId { get; set; }

        /// <summary>
        /// Metas do mentorado.
        /// </summary>
        [Required(ErrorMessage = "As metas são obrigatórias.")]
        [StringLength(1500, ErrorMessage = "As metas devem ter no máximo 1500 caracteres.")]
        public string Goals { get; set; } = string.Empty;

        /// <summary>
        /// Interesses do mentorado.
        /// </summary>
        [Required(ErrorMessage = "Os interesses são obrigatórios.")]
        [StringLength(1500, ErrorMessage = "Os interesses devem ter no máximo 1500 caracteres.")]
        public string Interests { get; set; } = string.Empty;

        /// <summary>
        /// Horas semanais disponíveis para mentoria.
        /// </summary>
        [Required(ErrorMessage = "Horas semanais disponíveis são obrigatórias.")]
        [Range(1, 80, ErrorMessage = "As horas semanais devem estar entre 1 e 80.")]
        public int WeeklyHoursAvailable { get; set; }

        /// <summary>
        /// Data da submissão do questionário.
        /// </summary>
        public DateTime SubmittedAt { get; set; }

        /// <summary>
        /// Links HATEOAS do recurso.
        /// </summary>
        public List<LinkDto> Links { get; set; } = new();
    }
}
