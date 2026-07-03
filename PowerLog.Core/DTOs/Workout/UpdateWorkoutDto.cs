using System.ComponentModel.DataAnnotations;

namespace PowerLog.Core.DTOs.Workout
{
    public class UpdateWorkoutDto
    {
        [Required]
        public DateTime Planned { get; set; }

        public DateTime? Actual { get; set; }
    }
}
