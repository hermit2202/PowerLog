namespace PowerLog.Core.Models
{
    /// <summary>
    /// Модель выполненных упражнений за конкретную тренировку.
    /// </summary>
    public class WorkoutExercise
    {
        /// <summary>
        /// Уникальный идентификатор выполненных упражнений за тренировку.
        /// </summary>
        public Guid WorkoutExerciseId { get; set; }

        /// <summary>
        /// Уникальный идентификатор упражнения.
        /// </summary>
        public Guid ExerciseId { get; set; }

        /// <summary>
        /// Уникальный идентификатор тренировки.
        /// </summary>
        public Guid WorkoutId { get; set; }

        /// <summary>
        /// Упражнение.
        /// </summary>
        public Exercise Exercise { get; set; }

        /// <summary>
        /// Тренировка.
        /// </summary>
        public Workout Workout { get; set; }

        /// <summary>
        /// Порядок выполнения упражнений.
        /// </summary>
        public int Order { get; set; }

        /// <summary>
        /// Количество подходов.
        /// </summary>
        public List<Set> Sets { get; set; }
    }
}
