using System.ComponentModel.DataAnnotations;

namespace PowerLog.Core.DTOs.PersonalRecord
{
    public class UpdatePersonalRecordDto
    {

        [Required]
        public Guid ExerciseId { get; set; }

        [Range(0.00, 2000.00)]
        public decimal Weight { get; set; }

        [Range(1, 200)]
        public int Reps { get; set; }

        [Range(0, 10)]
        public int Rpe { get; set; }
    }
}
