using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mentorax.Api.Models
{
    /// <summary>
    /// Entidade que representa um questionário respondido pelo mentorado.
    /// </summary>
    public class Questionario
    {
        /// <summary>
        /// Identificador único do questionário.
        /// </summary>
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        /// <summary>
        /// ID do mentorado ao qual este questionário pertence.
        /// </summary>
        [Required(ErrorMessage = "MenteeId é obrigatório")]
        public Guid MenteeId { get; set; }

        /// <summary>
        /// Metas profissionais ou acadêmicas informadas pelo mentorado.
        /// </summary>
        [Required(ErrorMessage = "Goals é obrigatório")]
        [MaxLength(2000, ErrorMessage = "Goals deve ter no máximo 2000 caracteres")]
        public string Goals { get; set; } = string.Empty;

        /// <summary>
        /// Interesses indicados pelo mentorado.
        /// </summary>
        [Required(ErrorMessage = "Interests é obrigatório")]
        [MaxLength(2000, ErrorMessage = "Interests deve ter no máximo 2000 caracteres")]
        public string Interests { get; set; } = string.Empty;

        /// <summary>
        /// Quantidade de horas disponíveis por semana para estudo/mentoria.
        /// </summary>
        [Range(0, 168, ErrorMessage = "WeeklyHoursAvailable deve estar entre 0 e 168")]
        public int WeeklyHoursAvailable { get; set; }

        /// <summary>
        /// Data em que o questionário foi enviado.
        /// </summary>
        [Required]
        public DateTime SubmittedAt { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Propriedade de navegação para o mentorado.
        /// </summary>
        [ForeignKey(nameof(MenteeId))]
        public virtual Mentorado? Mentorado { get; set; }
    }
}
