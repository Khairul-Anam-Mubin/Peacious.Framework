using Microsoft.Extensions.DependencyInjection;

namespace Peacious.Framework.IdentityScope;

public static class DependencyInjection
{
    public static IServiceCollection AddIdentityScopeContext(this IServiceCollection services)
    {
        services.AddHttpContextAccessor();
        services.AddTransient<IdentityScopeContextMiddleware>();
        services.AddScoped<IIdentityScopeContext, IdentityScopeContext>();
        return services;
    }
}
