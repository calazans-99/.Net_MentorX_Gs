using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mentorax.Api.Models
{
    /// <summary>
    /// Entidade que representa um mentorado no sistema.
    /// </summary>
    public class Mentorado
    {
        /// <summary>
        /// Identificador único do mentorado.
        /// </summary>
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        /// <summary>
        /// Nome completo do mentorado.
        /// </summary>
        [Required]
        [MaxLength(255)]
        public string FullName { get; set; } = string.Empty;

        /// <summary>
        /// Email do mentorado.
        /// </summary>
        [Required]
        [MaxLength(255)]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        /// <summary>
        /// Departamento do mentorado.
        /// </summary>
        [Required]
        [MaxLength(255)]
        public string Department { get; set; } = string.Empty;

        /// <summary>
        /// Data de criação do registro.
        /// </summary>
        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
