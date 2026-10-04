using PowerLog.Core.Contracts.Data; // Путь к твоему IUnitOfWork

namespace PowerLog.Infrastructure.Persistence;

/// <summary>
/// Реализация паттерна Unit of Work для сохранения изменений
/// </summary>
public class UnitOfWork : IUnitOfWork
{
    private readonly PowerLogContext context;

    public UnitOfWork(PowerLogContext context)
    {
        this.context = context;
    }

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await context.SaveChangesAsync(cancellationToken);
    }
}
