namespace PowerLog.Core.Contracts.Data;

/// <summary>
/// Интерфейс для получения данных из контекста.
/// </summary>
public interface IReader
{
    /// <summary>
    /// Предоставляет функциональную возможность для выполнения запросов.
    /// </summary>
    IQueryable<TEntity> Read<TEntity>() where TEntity : class, IEntity;
}
