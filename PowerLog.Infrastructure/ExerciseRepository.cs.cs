using Microsoft.EntityFrameworkCore;
using PowerLog.Core.Contracts.Data;
using PowerLog.Core.Models;

namespace PowerLog.Infrastructure;

public class ExerciseRepository : BaseWriteRepository<Exercise>, IExerciseRepository
{
    private readonly IReader reader;

    public ExerciseRepository(IWriter writer, IReader reader) : base(writer)
    {
        this.reader = reader;
    }

    public async Task<IEnumerable<Exercise>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await reader.Read<Exercise>()
            .ToListAsync(cancellationToken);
    }

    public async Task<Exercise?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await reader.Read<Exercise>()
            .FirstOrDefaultAsync(e => e.ExerciseId == id, cancellationToken);
    }

    public async Task<Exercise?> GetByNameAsync(string name, CancellationToken cancellationToken)
    {
        return await reader.Read<Exercise>()
            .FirstOrDefaultAsync(e => e.Name == name, cancellationToken);
    }
}
