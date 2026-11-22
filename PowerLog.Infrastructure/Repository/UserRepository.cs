using Microsoft.EntityFrameworkCore;
using PowerLog.Core.Contracts.IData;
using PowerLog.Core.Models;

namespace PowerLog.Infrastructure.Repository;

public class UserRepository : BaseWriteRepository<User>, IUserRepository
{
    private readonly IReader reader;

    public UserRepository(IWriter writer, IReader reader) : base(writer)
    {
        this.reader = reader;
    }

    public async Task<User?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await reader.Read<User>()
            .FirstOrDefaultAsync(u => u.UserId == id, cancellationToken);
    }

    public async Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken)
    {
        return await reader.Read<User>()
            .FirstOrDefaultAsync(u => u.Email == email, cancellationToken);
    }

    public async Task<User?> GetByIdWithWorkoutsAsync(Guid id, CancellationToken cancellationToken)
    {
        return await reader.Read<User>()
            .Include(u => u.Workouts)
            .FirstOrDefaultAsync(u => u.UserId == id, cancellationToken);
    }
}
