using Microsoft.EntityFrameworkCore;
using PowerLog.Core.Contracts.Data;
using PowerLog.Core.Models;

namespace PowerLog.Infrastructure;

public class ExerciseRepository : BaseWriteRepository<Exercise>, IExerciseRepository
{
    private readonly IReader reader;

    private IQueryable<Exercise> Available(Guid userId)
    {
        return reader.Read<Exercise>()
            .Where(e => (e.UserId == null || e.UserId == userId) && !e.IsDeleted);
    }

    public ExerciseRepository(IWriter writer, IReader reader) : base(writer)
    {
        this.reader = reader;
    }

    public async Task<IEnumerable<Exercise>> GetAvailableAsync(Guid userId, CancellationToken cancellationToken)
    {
        return await Available(userId)
            .OrderBy(e => e.Name)
            .ToListAsync(cancellationToken);
    }

    public async Task<Exercise?> GetAvailableByIdAsync(Guid id, Guid userId, CancellationToken cancellationToken)
    {
        return await Available(userId)
            .FirstOrDefaultAsync(e => e.ExerciseId == id, cancellationToken);
    }

    public async Task<bool> NameExistsAsync(string name, Guid userId, Guid? excludeId, CancellationToken cancellationToken)
    {
        var normalized = name.Trim().ToLower();

        return await Available(userId)
            .AnyAsync(e => e.Name.Trim().ToLower() == normalized
                && (excludeId == null || e.ExerciseId != excludeId),
                cancellationToken);
    }

    public async Task<bool> IsUsedAsync(Guid id, CancellationToken cancellationToken)
    {
        var hasRecords = await reader.Read<PersonalRecord>()
            .AnyAsync(pr => pr.ExerciseId == id, cancellationToken);

        if (hasRecords)
        {
            return true;
        }

        return await reader.Read<WorkoutExercise>()
            .AnyAsync(we => we.ExerciseId == id, cancellationToken);
    }
}
