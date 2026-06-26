using System.ComponentModel.DataAnnotations;

namespace PowerLog.Core.DTOs.Workout
{
    public class CreateWorkoutDto
    {
        [Required]
        public Guid UserId { get; set; }

        [Required]
        public DateTime Planned { get; set; }

        public DateTime? Actual { get; set; }
    }
}
