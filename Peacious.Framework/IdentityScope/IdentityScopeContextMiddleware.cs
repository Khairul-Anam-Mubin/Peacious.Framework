﻿using Microsoft.AspNetCore.Http;
using Peacious.Framework.Extensions;

namespace Peacious.Framework.IdentityScope;

public class IdentityScopeContextMiddleware(
    IIdentityScopeContext currentTokenContext) : IMiddleware
{
    private readonly IIdentityScopeContext _currentTokenContext = currentTokenContext;

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        _currentTokenContext.Initiate(context.GetAccessToken());

        await next(context);
    }
}