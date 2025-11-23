using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace Mentorax.Api.Models.Dto
{
    /// <summary>
    /// DTO para resposta de mentor
    /// </summary>
    public class MentorDto
    {
        /// <summary>
        /// Identificador único do mentor
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Nome completo do mentor
        /// </summary>
        [Required(ErrorMessage = "Nome completo é obrigatório")]
        [StringLength(255, ErrorMessage = "Nome deve ter no máximo 255 caracteres")]
        public string FullName { get; set; } = string.Empty;

        /// <summary>
        /// Email do mentor
        /// </summary>
        [Required(ErrorMessage = "Email é obrigatório")]
        [EmailAddress(ErrorMessage = "Email inválido")]
        [StringLength(255, ErrorMessage = "Email deve ter no máximo 255 caracteres")]
        public string Email { get; set; } = string.Empty;

        /// <summary>
        /// Área de especialização do mentor
        /// </summary>
        [Required(ErrorMessage = "Área de especialização é obrigatória")]
        [StringLength(255, ErrorMessage = "Especialização deve ter no máximo 255 caracteres")]
        public string Expertise { get; set; } = string.Empty;

        /// <summary>
        /// Data de criação do registro
        /// </summary>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Links HATEOAS associados ao recurso
        /// </summary>
        public List<LinkDto> Links { get; set; } = new List<LinkDto>();
    }
}
