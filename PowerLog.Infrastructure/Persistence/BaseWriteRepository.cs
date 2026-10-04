using PowerLog.Core.Contracts.Data;
using PowerLog.Core.Models;

namespace PowerLog.Infrastructure;

/// <summary>
/// Базовый класс репозитория записи данных.
/// </summary>
public abstract class BaseWriteRepository<T> : IBaseWriteRepository<T>
    where T : class, IEntity
{
    private readonly IWriter writer;

    protected BaseWriteRepository(IWriter writer)
    {
        this.writer = writer;
    }

    public void Add(T entity) => writer.Add(entity);
    public void Update(T entity) => writer.Update(entity);
    public void Delete(T entity) => writer.Delete(entity);
}
