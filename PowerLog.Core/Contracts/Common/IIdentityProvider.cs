namespace PowerLog.Core.Contracts.Common;

public interface IIdentityProvider
{
    Guid? CurrentUserId { get; }
}
