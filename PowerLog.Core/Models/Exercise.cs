using System.ComponentModel.DataAnnotations;

namespace PowerLog.Core.Models
{
    /// <summary>
    /// Модель упражнения
    /// </summary>
    public class Exercise : IEntity
    {
        /// <summary>
        /// Уникальный идентификатор упражнения.
        /// </summary>
        public Guid ExerciseId { get; set; }

        /// <summary>
        /// Название упражнения.
        /// </summary>
        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Тип упражнения.
        /// </summary>
        public ExerciseType Type { get; set; }

        /// <summary>
        /// Описание упражнения.
        /// </summary>
        [StringLength(500)]
        public string? Description { get; set; }
    }

    /// <summary>
    /// Типы упражнений.
    /// </summary>
    public enum ExerciseType
    {
        Squat,
        BenchPress,
        DeadLift,
        Accessory
    }
}
