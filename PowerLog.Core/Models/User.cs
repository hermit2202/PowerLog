using System.ComponentModel.DataAnnotations;

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
        [Required]
        [StringLength(50)]
        public string UserName { get; set; } = string.Empty;

        /// <summary>
        /// Почта.
        /// </summary>
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        /// <summary>
        /// Пароль.
        /// </summary>
        [Required]
        public string PasswordHash { get; set; } = string.Empty;

        /// <summary>
        /// Персональные рекорды пользователя.
        /// </summary>
        public List<PersonalRecord> PersonalRecords { get; set; } = new List<PersonalRecord>();

        /// <summary>
        /// Список тренировок пользователя.
        /// </summary>
        public List<Workout> Workouts { get; set; } = new List<Workout>();

        /// <summary>
        /// Роль пользователя: спортсмен или тренер.
        /// </summary>
        public UserRole Role { get; set; }
    }
}
