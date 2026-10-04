using PowerLog.Core.Models;

namespace PowerLog.Core.Contracts.Data;

/// <summary>
/// Интерфейс для получения данных из контекста.
/// </summary>
public interface IReader
{
    /// <summary>
    /// Предоставляет IQueryable для построения запросов.
    /// Реализация должна по умолчанию использовать AsNoTracking() для оптимизации чтения.
    /// </summary>
    IQueryable<TEntity> Read<TEntity>() where TEntity : class, IEntity;
}
