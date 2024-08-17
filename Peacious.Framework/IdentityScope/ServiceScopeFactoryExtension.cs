using Microsoft.Extensions.DependencyInjection;

namespace Peacious.Framework.IdentityScope;

public static class ServiceScopeFactoryExtension
{
    public static IServiceScope InitiateNewIdentityScopeContext(this IServiceScopeFactory serviceScopeFactory, string token)
    {
        var scope = serviceScopeFactory.CreateScope();

        var identityScopeContext =
            scope.ServiceProvider.GetRequiredService<IIdentityScopeContext>();

        identityScopeContext.Initiate(token);

        return scope;
    }
}
