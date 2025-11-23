using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mentorax.Api.Models
{
    /// <summary>
    /// Entidade que representa um plano de mentoria.
    /// </summary>
    public class PlanoMentoria
    {
        /// <summary>
        /// Identificador único do plano de mentoria.
        /// </summary>
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        /// <summary>
        /// ID do mentorado relacionado ao plano de mentoria.
        /// </summary>
        [Required(ErrorMessage = "O campo MenteeId é obrigatório.")]
        public Guid MenteeId { get; set; }

        /// <summary>
        /// Título do plano de mentoria.
        /// </summary>
        [Required(ErrorMessage = "O título é obrigatório.")]
        [MaxLength(255, ErrorMessage = "O título deve ter no máximo 255 caracteres.")]
        public string Title { get; set; } = string.Empty;

        /// <summary>
        /// Resumo geral do plano de mentoria.
        /// </summary>
        [Required(ErrorMessage = "O resumo é obrigatório.")]
        [MaxLength(2000, ErrorMessage = "O resumo deve ter no máximo 2000 caracteres.")]
        public string Summary { get; set; } = string.Empty;

        /// <summary>
        /// Cronograma semanal em formato JSON.
        /// </summary>
        [Required(ErrorMessage = "O cronograma semanal é obrigatório.")]
        public string WeeklyScheduleJson { get; set; } = string.Empty;

        /// <summary>
        /// Data de criação do plano.
        /// </summary>
        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // ------------------------------
        // RELACIONAMENTO
        // ------------------------------

        /// <summary>
        /// Mentorado ao qual o plano pertence.
        /// </summary>
        [ForeignKey(nameof(MenteeId))]
        public virtual Mentorado Mentorado { get; set; } = null!;
    }
}
