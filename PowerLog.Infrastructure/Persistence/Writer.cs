using PowerLog.Core.Models;
using PowerLog.Core.Contracts.Data;

namespace PowerLog.Infrastructure.Persistence;

/// <summary>
/// Реализация интерфейса записи данных в контекст
/// </summary>
public class Writer : IWriter
{
    private readonly PowerLogContext context;

    public Writer(PowerLogContext context)
    {
        this.context = context;
    }

    public void Add<TEntity>(TEntity entity) where TEntity : class, IEntity
    {
        context.Set<TEntity>().Add(entity);
    }

    public void Update<TEntity>(TEntity entity) where TEntity : class, IEntity
    {
        context.Set<TEntity>().Update(entity);
    }

    public void Delete<TEntity>(TEntity entity) where TEntity : class, IEntity
    {
        context.Set<TEntity>().Remove(entity);
    }
}
