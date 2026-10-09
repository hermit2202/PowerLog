namespace PowerLog.Core.Contracts.Common;

public interface IDateTimeProvider
{
    DateTime Now { get; }
}
