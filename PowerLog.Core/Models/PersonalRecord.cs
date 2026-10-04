using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PowerLog.Core.Models
{
    /// <summary>
    /// Модель персональных рекордов.
    /// </summary>
    public class PersonalRecord : IEntity
    {
        /// <summary>
        /// Уникальный идентификатор рекорда.
        /// </summary>
        public Guid PersonalRecordId { get; set; }

        /// <summary>
        /// Уникальный идентификатор пользователя.
        /// </summary>
        [Required]
        public Guid UserId { get; set; }

        /// <summary>
        /// Уникальный идентификатор упражнения.
        /// </summary>
        [Required]
        public Guid ExerciseId { get; set; }

        /// <summary>
        /// Пользователь.
        /// </summary>
        public User? User { get; set; }

        /// <summary>
        /// Упражнение.
        /// </summary>
        public Exercise? Exercise { get; set; }

        /// <summary>
        /// Вес снаряда.
        /// </summary>
        [Column(TypeName = "decimal(6,2)")]
        [Range(0.00, 2000.00)]
        public decimal Weight { get; set; }

        /// <summary>
        /// Количество повторений.
        /// </summary>
        [Range(1, 200)]
        public int Reps { get; set; }

        /// <summary>
        /// Шкала субъективной оценки усилий во время физической нагрузки.
        /// </summary>
        [Range(0, 10)]
        public int Rpe { get; set; }

        /// <summary>
        /// Дата установленного рекорда.
        /// </summary>
        [Required]
        public DateTime RecordDate { get; set; }
    }
}
