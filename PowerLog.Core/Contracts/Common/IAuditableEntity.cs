namespace PowerLog.Core.Contracts.Common;

public interface IAuditableEntity
{
    DateTime CreatedAt { get; set; }
    Guid? CreatedBy { get; set; }
    DateTime UpdatedAt { get; set; }
    Guid? UpdatedBy { get; set; }
}
