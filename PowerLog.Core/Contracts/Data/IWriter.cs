namespace PowerLog.Core.Contracts.Data;

/// <summary>
/// Интерфейс для создания и модификации записей в контексте.
/// </summary>
public interface IWriter
{
    /// <summary>
    /// Добавить новую запись.
    /// </summary>
    void Add<TEntity>(TEntity entity) where TEntity : class, TEntity;

    /// <summary>
    /// Изменить запись.
    /// </summary>
    void Update<TEntity>(TEntity entity) where TEntity : class, TEntity;

    /// <summary>
    /// Удалить запись.
    /// </summary>
    void Delete<TEntity>(TEntity entity) where TEntity : class, TEntity;
}
