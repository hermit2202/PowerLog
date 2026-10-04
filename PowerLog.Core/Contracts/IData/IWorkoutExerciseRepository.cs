using PowerLog.Core.Models;

namespace PowerLog.Core.Contracts.Data;

public interface IWorkoutExerciseRepository : IBaseWriteRepository<WorkoutExercise>
{
    Task<IEnumerable<WorkoutExercise>> GetByWorkoutIdAsync(Guid workoutId, CancellationToken cancellationToken);
}
