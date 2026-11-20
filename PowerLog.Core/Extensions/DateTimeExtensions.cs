namespace PowerLog.Core.Extensions;

/// <summary>
/// Метод расширения для работы с датой и временем.
/// </summary>
public static class DateTimeExtensions
{
    /// <summary>
    /// Приводит дату к UTC перед сохранением в базу данных.
    /// </summary>
    /// <param name="value">Исходная дата.</param>
    /// <returns>
    /// Дата без указания часового пояса (<see cref="DateTimeKind.Unspecified"/>)
    /// считается уже указанной в UTC: время не меняется, только ставится метка.
    /// Дата в UTC или в локальном поясе пересчитывается в UTC.
    /// Так результат не зависит от часового пояса сервера.
    /// </returns>
    public static DateTime ToUtc(this DateTime value)
    {
        if (value.Kind == DateTimeKind.Unspecified)
        {
            return DateTime.SpecifyKind(value, DateTimeKind.Utc);
        }

        return value.ToUniversalTime();
    }
}
