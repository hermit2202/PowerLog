using System.Security.Claims;
using PowerLog.Core.Contracts.Common;

namespace PowerLog.Api.Providers;

public class IdentityProvider : IIdentityProvider
{
    private readonly IHttpContextAccessor httpContextAccessor;

    public IdentityProvider(IHttpContextAccessor httpContextAccessor)
    {
        this.httpContextAccessor = httpContextAccessor;
    }

    public Guid? CurrentUserId
    {
        get
        {
            var userIdClaim = httpContextAccessor.HttpContext?.User
                ?.FindFirst(ClaimTypes.NameIdentifier)
                ?.Value;
            return Guid.TryParse(userIdClaim, out var userId) ? userId : null;
        }
    }
}
