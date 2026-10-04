using PowerLog.Core.Models;

namespace PowerLog.Core.Contracts.Data;

public interface IWorkoutRepository : IBaseWriteRepository<Workout>
{
    Task<Workout?> GetByIdWithDetailsAsync(Guid id, CancellationToken cancellationToken);
    Task<IEnumerable<Workout>> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken);
}
