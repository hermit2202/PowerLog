using PowerLog.Core.Models;

namespace PowerLog.Core.Contracts.IData;

public interface IWorkoutExerciseRepository : IBaseWriteRepository<WorkoutExercise>
{
    Task<IEnumerable<WorkoutExercise>> GetByWorkoutIdAsync(Guid workoutId, CancellationToken cancellationToken);
}
