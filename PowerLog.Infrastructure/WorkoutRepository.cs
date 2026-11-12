using Microsoft.EntityFrameworkCore;
using PowerLog.Core.Contracts.Data;
using PowerLog.Core.Models;

namespace PowerLog.Infrastructure;

public class WorkoutRepository : BaseWriteRepository<Workout>, IWorkoutRepository
{
    private readonly IReader reader;

    public WorkoutRepository(IWriter writer, IReader reader) : base(writer)
    {
        this.reader = reader;
    }

    public async Task<Workout?> GetByIdWithDetailsAsync(Guid id, Guid userId, CancellationToken cancellationToken)
    {
        return await reader.Read<Workout>()
            .Include(w => w.WorkoutExercises)
            .ThenInclude(we => we.Sets)
            .FirstOrDefaultAsync(w => w.WorkoutId == id && w.UserId == userId, cancellationToken);
    }

    public async Task<IEnumerable<Workout>> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken)
    {
        return await reader.Read<Workout>()
            .Where(w => w.UserId == userId)
            .OrderByDescending(w => w.Planned)
            .ToListAsync(cancellationToken);
    }
}
