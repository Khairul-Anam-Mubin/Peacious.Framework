using Microsoft.AspNetCore.Http;
using Peacious.Framework.Extensions;

namespace Peacious.Framework.Identity;

public class IdentityMiddleware : IMiddleware
{
    private readonly IScopeIdentity _scopeIdentity;

    public IdentityMiddleware(IScopeIdentity scopeIdentity)
    {
        _scopeIdentity = scopeIdentity;
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        _scopeIdentity.SwitchIdentity(context.GetAccessToken());

        await next(context);
    }
}