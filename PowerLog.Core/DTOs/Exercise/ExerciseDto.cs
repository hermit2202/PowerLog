using PowerLog.Core.Models;

namespace PowerLog.Core.DTOs.Exercise
{
    public class ExerciseDto
    {
        public Guid ExerciseId { get; set; }
        public string Name { get; set; } = string.Empty;
        public ExerciseType Type { get; set; }
        public string? Description { get; set; }
    }
}
