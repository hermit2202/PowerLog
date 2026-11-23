using Xunit;
using PowerLog.Core.Extensions;

namespace PowerLog.UnitTests.Extensions;

public class DateTimeExtensionsTests
{
    [Fact]
    public void ToUtc_UnspecifiedKind_KeepsTimeAndSetsUtc()
    {
        // Arrange
        var unspecifiedDate = new DateTime(2026, 11, 20, 18, 0, 0,DateTimeKind.Unspecified);

        // Act
        var result = unspecifiedDate.ToUtc();

        // Assert
        Assert.Equal(DateTimeKind.Utc, result.Kind);
        Assert.Equal(unspecifiedDate.Ticks, result.Ticks);
    }

    [Fact]
    public void ToUtc_UtcKind_ReturnsSameValue()
    {
        // Arrange
        var utcDate = new DateTime(2026, 11, 20, 18, 0, 0, DateTimeKind.Utc);

        // Act
        var result = utcDate.ToUtc();

        // Assert
        Assert.Equal(utcDate.Kind, result.Kind);
        Assert.Equal(utcDate.Ticks, result.Ticks);
    }

    [Fact]
    public void ToUtc_LocalKind_ConvertsToUtc()
    {
        // Arrange
        var localDate = new DateTime(2026, 11, 20, 18, 0, 0, DateTimeKind.Local);
        var expected = localDate.ToUniversalTime();

        // Act
        var result = localDate.ToUtc();

        // Assert
        Assert.Equal(DateTimeKind.Utc, result.Kind);
        Assert.Equal(expected.Ticks, result.Ticks);
    }
}
