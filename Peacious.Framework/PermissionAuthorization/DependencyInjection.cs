using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;

namespace Peacious.Framework.PermissionAuthorization;

public static class DependencyInjection
{
    public static IServiceCollection AddPermissionAuthorization(this IServiceCollection services, IPermissionProvider permissionProvider)
    {
        services.AddScoped(typeof(IPermissionProvider), permissionProvider.GetType());
        services.AddScoped<IAuthorizationHandler, PermissionAuthorizationHandler>();
        services.AddSingleton<IAuthorizationPolicyProvider, PermisisonAuthorizationPolicyProvider>();
        return services;
    }
}
