using Microsoft.AspNetCore.Builder;

namespace Peacious.Framework.IdentityScope;

public static class ApplicationBuilderExtension
{
    public static IApplicationBuilder UseIdentityScopeContext(this IApplicationBuilder app)
    {
        app.UseMiddleware<IdentityScopeContextMiddleware>();
        return app;
    }
}
