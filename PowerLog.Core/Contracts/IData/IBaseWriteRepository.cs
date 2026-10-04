using PowerLog.Core.Models;

namespace PowerLog.Core.Contracts.Data;

/// <summary>
/// Базовый интерфейс репозитория записи сущности.
/// </summary>
public interface IBaseWriteRepository<T> where T : class, IEntity
{
    void Add(T entity);
    void Update(T entity);
    void Delete(T entity);
}
