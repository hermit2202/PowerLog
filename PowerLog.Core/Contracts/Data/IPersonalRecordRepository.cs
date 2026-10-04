using PowerLog.Core.Models;

namespace PowerLog.Core.Contracts.Data;

public interface IPersonalRecordRepository : IBaseWriteRepository<PersonalRecord>
{
    Task<IEnumerable<PersonalRecord>> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken);
    Task<PersonalRecord?> GetRecordAsync(Guid userId, Guid exerciseId, CancellationToken cancellationToken);
    Task<PersonalRecord?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IEnumerable<PersonalRecord>> GetByUserIdAndExerciseIdAsync(Guid userId, Guid exerciseId, CancellationToken cancellationToken);
}
