namespace PowerLog.Core.Models
{
    /// <summary>
    /// Модель пользователя.
    /// </summary>
    public class User
    {
        /// <summary>
        /// Уникальный идентификатор пользователя.
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// Имя пользователя.
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Персональные рекорды пользователя.
        /// </summary>
        public List<PersonalRecord> PersonalRecords { get; set; }

        /// <summary>
        /// Список тренировок пользователя.
        /// </summary>
        public List<Workout> Workouts { get; set; }
    }
}
