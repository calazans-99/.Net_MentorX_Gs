using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace Mentorax.Api.Models.Dto
{
    /// <summary>
    /// DTO para resposta de mentorado
    /// </summary>
    public class MentoradoDto
    {
        /// <summary>
        /// Identificador único do mentorado
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Nome completo do mentorado
        /// </summary>
        [Required(ErrorMessage = "Nome completo é obrigatório")]
        [StringLength(255, ErrorMessage = "Nome deve ter no máximo 255 caracteres")]
        public string FullName { get; set; } = string.Empty;

        /// <summary>
        /// Email do mentorado
        /// </summary>
        [Required(ErrorMessage = "Email é obrigatório")]
        [EmailAddress(ErrorMessage = "Email inválido")]
        [StringLength(255, ErrorMessage = "Email deve ter no máximo 255 caracteres")]
        public string Email { get; set; } = string.Empty;

        /// <summary>
        /// Departamento ou área do mentorado
        /// </summary>
        [Required(ErrorMessage = "Departamento é obrigatório")]
        [StringLength(255, ErrorMessage = "Departamento deve ter no máximo 255 caracteres")]
        public string Department { get; set; } = string.Empty;

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
