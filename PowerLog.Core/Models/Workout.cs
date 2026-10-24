using System.ComponentModel.DataAnnotations;
using PowerLog.Core.Contracts.Common;

namespace PowerLog.Core.Models
{
    /// <summary>
    /// Модель тренировки.
    /// </summary>
    public class Workout : IEntity, IAuditableEntity
    {
        /// <summary>
        /// Уникальный идентификатор тренировки.
        /// </summary>
        public Guid WorkoutId { get; set; }

        /// <summary>
        /// Уникальный идентификатор пользователя.
        /// </summary>
        [Required]
        public Guid UserId { get; set; }

        /// <summary>
        /// Пользователь.
        /// </summary>
        public User? User { get; set; }

        /// <summary>
        /// Планируемая дата тренировки.
        /// </summary>
        [Required]
        public DateTime Planned { get; set; }

        /// <summary>
        /// Фактическая дата тренировки.
        /// </summary>
        public DateTime? Actual { get; set; }

        /// <summary>
        /// Сипсок выполнненых упрежнений за тренировку.
        /// </summary>
        public List<WorkoutExercise> WorkoutExercises { get; set; } = new List<WorkoutExercise>();

        /// <summary>
        /// Поля аудита.
        /// </summary>
        public DateTime CreatedAt { get; set; }
        public Guid? CreatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }
        public Guid? UpdatedBy { get; set; }
    }
}
