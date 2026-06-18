namespace PowerLog.Core.Models
{
    /// <summary>
    /// Модель персональных рекордов.
    /// </summary>
    public class PersonalRecord
    {
        /// <summary>
        /// Уникальный идентификатор рекорда.
        /// </summary>
        public Guid RecordId { get; set; }

        /// <summary>
        /// Уникальный идентификатор пользователя.
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// Уникальный идентификатор упражнения.
        /// </summary>
        public Guid ExerciseId { get; set; }

        /// <summary>
        /// Пользователь.
        /// </summary>
        public User User { get; set; }

        /// <summary>
        /// Упражнение.
        /// </summary>
        public Exercise Exercise { get; set; }

        /// <summary>
        /// Вес снаряда.
        /// </summary>
        public decimal Weight { get; set; }

        /// <summary>
        /// Количество повторений.
        /// </summary>
        public int Reps { get; set; }

        /// <summary>
        /// Шкала субъективной оценки усилий во время физической нагрузки.
        /// </summary>
        public int? RPE { get; set; }

        /// <summary>
        /// Дата установленного рекорда.
        /// </summary>
        public DateTime RecordDate { get; set; }
    }
}
