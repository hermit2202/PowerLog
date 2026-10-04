using Microsoft.EntityFrameworkCore;
using PowerLog.Core.Contracts.Data;
using PowerLog.Core.Models;

namespace PowerLog.Infrastructure;

public class WorkoutExerciseRepository : BaseWriteRepository<WorkoutExercise>, IWorkoutExerciseRepository
{
    private readonly IReader reader;

    public WorkoutExerciseRepository(IWriter writer, IReader reader) : base(writer)
    {
        this.reader = reader;
    }

    public async Task<IEnumerable<WorkoutExercise>> GetByWorkoutIdAsync(Guid workoutId, CancellationToken cancellationToken)
    {
        return await reader.Read<WorkoutExercise>()
            .Where(we => we.WorkoutId == workoutId)
            .Include(we => we.Exercise)
            .Include(we => we.Sets)
            .ToListAsync(cancellationToken);
    }
}
