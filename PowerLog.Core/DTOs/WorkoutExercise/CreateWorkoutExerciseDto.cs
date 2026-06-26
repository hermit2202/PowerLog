using System.ComponentModel.DataAnnotations;
using PowerLog.Core.DTOs.Set;

namespace PowerLog.Core.DTOs.WorkoutExercise
{
    public class CreateWorkoutExerciseDto
    {
        [Required]
        public Guid ExerciseId { get; set; }

        [Required]
        public Guid WorkoutId { get; set; }

        [Range(1, 100)]
        public int Order { get; set; }

        public List<CreateSetDto> Sets { get; set; } = new List<CreateSetDto>();
    }
}
