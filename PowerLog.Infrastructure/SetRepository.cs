using Microsoft.EntityFrameworkCore;
using PowerLog.Core.Contracts.Data;
using PowerLog.Core.Models;

namespace PowerLog.Infrastructure;

public class SetRepository : BaseWriteRepository<Set>, ISetRepository
{
    private readonly IReader reader;

    public SetRepository(IWriter writer, IReader reader) : base(writer)
    {
        this.reader = reader;
    }

    public async Task<IEnumerable<Set>> GetByWorkoutExerciseIdAsync(Guid workoutExerciseId, CancellationToken cancellationToken)
    {
        return await reader.Read<Set>()
            .Where(s => s.WorkoutExerciseId == workoutExerciseId)
            .OrderBy(s => s.SetNumber)
            .ToListAsync(cancellationToken);
    }
}
