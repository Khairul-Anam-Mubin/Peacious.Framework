using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Peacious.Framework.Extensions;

namespace Peacious.Framework.ServiceRegistration;

public static class DependencyInjection
{
    public static IServiceCollection AddAttributeRegisteredServices(this IServiceCollection services, params Assembly[] assemblies)
    {
        foreach (var assembly in assemblies)
        {
            services.AddAttributeRegisteredServices(assembly.GetExportedTypes());
        }

        return services;
    }

    public static IServiceCollection AddAttributeRegisteredServices(this IServiceCollection services, params Type[] types)
    {
        foreach (var type in types)
        {
            if (!type.CanInstantiate()) continue;

            var serviceRegisterAttributes = type.GetCustomAttributes<ServiceRegisterAttribute>();

            if (serviceRegisterAttributes.Any() == false) continue;

            foreach (var serviceRegisterAttribute in serviceRegisterAttributes)
            {
                services.RegisterService(type, serviceRegisterAttribute.ServiceLifetime, serviceRegisterAttribute.ServiceType, serviceRegisterAttribute.Key);
            }
        }

        return services;
    }
}