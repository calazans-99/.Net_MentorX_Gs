using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mentorax.Api.Models
{
    /// <summary>
    /// Entidade que representa uma tarefa pertencente a um plano de mentoria.
    /// </summary>
    public class TarefaMentoria
    {
        /// <summary>
        /// Identificador único da tarefa.
        /// </summary>
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        /// <summary>
        /// ID do plano de mentoria ao qual a tarefa pertence.
        /// </summary>
        [Required]
        public Guid MentorshipPlanId { get; set; }

        /// <summary>
        /// Descrição da tarefa.
        /// </summary>
        [Required]
        [MaxLength(500)]
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// Quantidade estimada de horas para realizar a tarefa.
        /// </summary>
        [Range(1, int.MaxValue, ErrorMessage = "EstimatedHours deve ser maior que zero")]
        public int EstimatedHours { get; set; }

        /// <summary>
        /// Status da tarefa (concluída ou não).
        /// </summary>
        [Required]
        public bool Done { get; set; }

        /// <summary>
        /// Data de criação do registro.
        /// </summary>
        [Required]
        public DateTime DataCriacao { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Data da última atualização do registro.
        /// </summary>
        public DateTime DataAtualizacao { get; set; } = DateTime.UtcNow;

        // ---------------------------
        //     NAVIGATION PROPERTY
        // ---------------------------

        /// <summary>
        /// Plano de mentoria ao qual esta tarefa pertence.
        /// </summary>
        [ForeignKey(nameof(MentorshipPlanId))]
        public virtual PlanoMentoria PlanoMentoria { get; set; } = null!;
    }
}
