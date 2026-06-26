using System.ComponentModel.DataAnnotations;
using PowerLog.Core.Models;

namespace PowerLog.Core.DTOs.Exercise
{
    public class CreateExerciseDto
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;
        public ExerciseType Type { get; set; }
        [StringLength(500)]
        public string? Description { get; set; }
    }
}
