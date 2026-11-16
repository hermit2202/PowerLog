using PowerLog.Core.Models;

namespace PowerLog.Core.Contracts.IData;

public interface IExerciseRepository : IBaseWriteRepository<Exercise>
{
    Task<IEnumerable<Exercise>> GetAvailableAsync(Guid userId, CancellationToken cancellationToken);
    Task<Exercise?> GetAvailableByIdAsync(Guid id, Guid userId, CancellationToken cancellationToken);
    Task<bool> NameExistsAsync(string name, Guid userId, Guid? excludeId, CancellationToken cancellationToken);
    Task<bool> IsUsedAsync(Guid id, CancellationToken cancellationToken);
}
