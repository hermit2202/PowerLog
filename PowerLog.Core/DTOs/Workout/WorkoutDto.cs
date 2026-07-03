using PowerLog.Core.DTOs.WorkoutExercise;

namespace PowerLog.Core.DTOs.Workout
{
    public class WorkoutDto
    {
        public Guid WorkoutId { get; set; }

        public Guid UserId { get; set; }

        public DateTime Planned { get; set; }

        public DateTime? Actual { get; set; }

        public List<WorkoutExerciseDto> Exercises { get; set; } = new List<WorkoutExerciseDto>();
    }
}
