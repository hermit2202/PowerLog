using PowerLog.Core.Models;

namespace PowerLog.Core.Contracts.Data;

public interface IUserRepository : IBaseWriteRepository<User>
{
    Task<User?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken);
    Task<User?> GetByIdWithWorkoutsAsync(Guid id, CancellationToken cancellationToken);
}
