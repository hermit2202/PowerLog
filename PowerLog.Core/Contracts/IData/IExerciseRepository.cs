using PowerLog.Core.Models;

namespace PowerLog.Core.Contracts.Data;

public interface IExerciseRepository : IBaseWriteRepository<Exercise>
{
    Task<Exercise?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IEnumerable<Exercise>> GetAllAsync(CancellationToken cancellationToken);
    Task<Exercise?> GetByNameAsync(string name, CancellationToken cancellationToken);
}
