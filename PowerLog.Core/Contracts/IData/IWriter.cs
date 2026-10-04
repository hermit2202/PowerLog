using PowerLog.Core.Models;

namespace PowerLog.Core.Contracts.Data;

/// <summary>
/// Интерфейс создания и модификации записей в контексте
/// </summary>
public interface IWriter
{
    void Add<TEntity>(TEntity entity) where TEntity : class, IEntity;
    void Update<TEntity>(TEntity entity) where TEntity : class, IEntity;
    void Delete<TEntity>(TEntity entity) where TEntity : class, IEntity;
}
