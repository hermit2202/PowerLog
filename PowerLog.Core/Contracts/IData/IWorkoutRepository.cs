using PowerLog.Core.Models;

namespace PowerLog.Core.Contracts.IData;

public interface IWorkoutRepository : IBaseWriteRepository<Workout>
{
    Task<Workout?> GetByIdWithDetailsAsync(Guid id, Guid userId, CancellationToken cancellationToken);
    Task<IEnumerable<Workout>> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken);
}
