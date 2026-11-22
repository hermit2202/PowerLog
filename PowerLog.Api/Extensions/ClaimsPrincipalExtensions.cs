using System.Security.Claims;

namespace PowerLog.Api.Extensions;

/// <summary>
/// Методы расширения для получения данных пользователя из токена.
/// </summary>
public static class ClaimsPrincipalExtensions
{
    /// <summary>
    /// Возвращает идентификатор пользователя из claim <see cref="ClaimTypes.NameIdentifier"/>.
    /// </summary>
    /// <param name="user">Пользователь из текущего запроса. Может быть <c>null</c>.</param>
    /// <returns>Идентификатор пользователя или <c>null</c>, если пользователь не аутентифицирован или claim отсутствует.</returns>
    /// <remarks>
    /// Используется там, где запрос может прийти без токена, например в аудите при регистрации.
    /// </remarks>
    public static Guid? FindUserId(this ClaimsPrincipal? user)
    {
        var userIdClaim = user
            ?.FindFirst(ClaimTypes.NameIdentifier)
            ?.Value;
        return Guid.TryParse(userIdClaim, out var userId) ? userId : null;
    }

    /// <summary>
    /// Возвращает идентификатор аутентифицированного пользователя.
    /// </summary>
    /// <param name="user">Пользователь из текущего запроса.</param>
    /// <returns>Идентификатор пользователя.</returns>
    /// <exception cref="InvalidOperationException">
    /// В токене нет идентификатора пользователя. На эндпоинтах с <c>[Authorize]</c>
    /// такого быть не должно, поэтому это ошибка сервера, а не клиента.
    /// </exception>
    public static Guid GetUserId(this ClaimsPrincipal user)
    {
        return user.FindUserId() ?? throw new InvalidOperationException("User ID not found in token.");
    }
}
