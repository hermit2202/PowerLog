using PowerLog.Api.Extensions;
using PowerLog.Core.Contracts.Common;

namespace PowerLog.Api.Providers;

public class IdentityProvider : IIdentityProvider
{
    private readonly IHttpContextAccessor httpContextAccessor;

    public IdentityProvider(IHttpContextAccessor httpContextAccessor)
    {
        this.httpContextAccessor = httpContextAccessor;
    }

    public Guid? CurrentUserId => httpContextAccessor.HttpContext?.User.FindUserId();
}
