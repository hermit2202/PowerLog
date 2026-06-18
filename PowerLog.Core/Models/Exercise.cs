namespace PowerLog.Core.Models
{
    /// <summary>
    /// Модель упражнения
    /// </summary>
    public class Exercise
    {
        /// <summary>
        /// Уникальный идентификатор упражнения.
        /// </summary>
        public Guid ExerciseId { get; set; }

        /// <summary>
        /// Название упражнения.
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// Тип упражнения.
        /// </summary>
        public ExerciseType Type { get; set; }

        /// <summary>
        /// Описание упражнения.
        /// </summary>
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
