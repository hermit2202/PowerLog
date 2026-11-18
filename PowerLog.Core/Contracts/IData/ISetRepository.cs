using PowerLog.Core.Models;

namespace PowerLog.Core.Contracts.IData;

public interface ISetRepository : IBaseWriteRepository<Set>
{
    Task<IEnumerable<Set>> GetByWorkoutExerciseIdAsync(Guid workoutExerciseId, CancellationToken cancellationToken);
}
