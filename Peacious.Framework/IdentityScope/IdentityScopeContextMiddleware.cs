using Microsoft.AspNetCore.Http;
using Peacious.Framework.Extensions;

namespace Peacious.Framework.IdentityScope;

public class IdentityScopeContextMiddleware(
    IIdentityScopeContext currentTokenContext) : IMiddleware
{
    private readonly IIdentityScopeContext _currentTokenContext = currentTokenContext;

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        var accessToken = context.GetAccessToken();

        if (!string.IsNullOrEmpty(accessToken))
        {
            _currentTokenContext.Initiate(accessToken);
        }

        await next(context);
    }
}