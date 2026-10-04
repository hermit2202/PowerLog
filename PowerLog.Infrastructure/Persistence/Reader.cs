using Microsoft.EntityFrameworkCore;
using PowerLog.Core.Models;
using PowerLog.Core.Contracts.Data;

namespace PowerLog.Infrastructure.Persistence;

/// <summary>
/// Реализация интерфейса чтения данных из контекста
/// </summary>
public class Reader : IReader
{
    private readonly PowerLogContext context;

    public Reader(PowerLogContext context)
    {
        this.context = context;
    }

    public IQueryable<TEntity> Read<TEntity>() where TEntity : class, IEntity
    {
        return context.Set<TEntity>().AsNoTracking();
    }
}
