using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PowerLog.Core.Models
{
    /// <summary>
    /// Модель выполненного подхода.
    /// </summary>
    public class Set
    {
        /// <summary>
        /// Уникальный идентификатор подхода.
        /// </summary>
        public Guid SetId { get; set; }

        /// <summary>
        /// Уникальный идентификатор выполненных упражнений за тренировку.
        /// </summary>
        public Guid WorkoutExerciseId { get; set; }

        /// <summary>
        /// Выполненное упражнения за тренировку.
        /// </summary>
        public WorkoutExercise? WorkoutExercise { get; set; }

        /// <summary>
        /// Вес сняряда.
        /// </summary>
        [Required]
        [Column(TypeName = "decimal(6,2)")]
        [Range(0.00, 2000.00)]
        public decimal Weight { get; set; }

        /// <summary>
        /// Количество выполенных повторений.
        /// </summary>
        [Range(1, 200)]
        public int Reps { get; set; }

        /// <summary>
        /// Шкала субъективной оценки усилий во время физической нагрузки.
        /// </summary>
        [Range(0, 10)]
        public int? Rpe { get; set; }
    }
}
