using System;
using System.ComponentModel.DataAnnotations;

namespace Mentorax.Api.Models
{
    /// <summary>
    /// Entidade que representa um mentor no sistema.
    /// </summary>
    public class Mentor
    {
        /// <summary>
        /// Identificador único do mentor.
        /// </summary>
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        /// <summary>
        /// Nome completo do mentor.
        /// </summary>
        [Required]
        [MaxLength(255)]
        public string FullName { get; set; } = string.Empty;

        /// <summary>
        /// Email do mentor.
        /// </summary>
        [Required]
        [MaxLength(255)]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        /// <summary>
        /// Área de especialização do mentor.
        /// </summary>
        [Required]
        [MaxLength(255)]
        public string Expertise { get; set; } = string.Empty;

        /// <summary>
        /// Data de criação do registro.
        /// </summary>
        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Data da última atualização.
        /// </summary>
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}
