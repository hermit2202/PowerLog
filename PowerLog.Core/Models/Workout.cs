namespace PowerLog.Core.Models
{
    /// <summary>
    /// Модель тренировки.
    /// </summary>
    public class Workout
    {
        /// <summary>
        /// Уникальный идентификатор тренировки.
        /// </summary>
        public Guid WorkoutId { get; set; }

        /// <summary>
        /// Уникальный идентификатор пользователя.
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// Пользователь.
        /// </summary>
        public User User { get; set; }

        /// <summary>
        /// Планируемая дата тренировки.
        /// </summary>
        public DateTime Planned { get; set; }

        /// <summary>
        /// Фактическая дата тренировки.
        /// </summary>
        public DateTime Actual { get; set; }

        /// <summary>
        /// Сипсок выполнненых упрежнений за тренировку.
        /// </summary>
        public List<WorkoutExercise> WorkoutExercises { get; set; }
    }
}
