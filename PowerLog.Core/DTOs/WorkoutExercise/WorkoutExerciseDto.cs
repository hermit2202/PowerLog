using PowerLog.Core.DTOs.Set;

namespace PowerLog.Core.DTOs.WorkoutExercise
{
    public class WorkoutExerciseDto
    {
        public Guid ExerciseId { get; set; }

        public Guid WorkoutExerciseId { get; set; }

        public Guid WorkoutId { get; set; }

        public int Order { get; set; }

        public List<SetDto> Sets { get; set; } = new List<SetDto>();
    }
}
