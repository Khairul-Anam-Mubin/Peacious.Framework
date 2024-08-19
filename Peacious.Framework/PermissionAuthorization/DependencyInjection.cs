using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;

namespace Peacious.Framework.PermissionAuthorization;

public static class DependencyInjection
{
    public static IServiceCollection AddPermissionAuthorization(this IServiceCollection services, Type permisionProviderImplementationType)
    {
        services.AddScoped(typeof(IPermissionProvider), permisionProviderImplementationType);
        services.AddScoped<IAuthorizationHandler, PermissionAuthorizationHandler>();
        services.AddSingleton<IAuthorizationPolicyProvider, PermisisonAuthorizationPolicyProvider>();
        return services;
    }
}
