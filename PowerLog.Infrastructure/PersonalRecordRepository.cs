using Microsoft.EntityFrameworkCore;
using PowerLog.Core.Contracts.Data;
using PowerLog.Core.Models;

namespace PowerLog.Infrastructure;

public class PersonalRecordRepository : BaseWriteRepository<PersonalRecord>, IPersonalRecordRepository
{
    private readonly IReader reader;

    public PersonalRecordRepository(IWriter writer, IReader reader) : base(writer)
    {
        this.reader = reader;
    }

    public async Task<IEnumerable<PersonalRecord>> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken)
    {
        return await reader.Read<PersonalRecord>()
            .Where(pr => pr.UserId == userId)
            .ToListAsync(cancellationToken);
    }

    public async Task<PersonalRecord?> GetRecordAsync(Guid userId, Guid exerciseId, CancellationToken cancellationToken)
    {
        return await reader.Read<PersonalRecord>()
            .FirstOrDefaultAsync(pr => pr.UserId == userId && pr.ExerciseId == exerciseId, cancellationToken);
    }

    public async Task<PersonalRecord?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await reader.Read<PersonalRecord>()
            .FirstOrDefaultAsync(pr => pr.PersonalRecordId == id, cancellationToken);
    }

    public async Task<IEnumerable<PersonalRecord>> GetByUserIdAndExerciseIdAsync(
        Guid userId, Guid exerciseId, CancellationToken cancellationToken)
    {
        return await reader.Read<PersonalRecord>()
            .Where(pr => pr.UserId == userId && pr.ExerciseId == exerciseId)
            .ToListAsync(cancellationToken);
    }
}
