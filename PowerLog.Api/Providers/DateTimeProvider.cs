using PowerLog.Core.Contracts.Common;

namespace PowerLog.Api.Providers;

public class DateTimeProvider : IDateTimeProvider
{
    public DateTime UtcNow => DateTime.UtcNow;
}
