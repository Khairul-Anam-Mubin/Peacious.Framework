using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Peacious.Framework.Extensions;

public static class ServiceCollectionExtension
{
    public static IServiceCollection RegisterService(this IServiceCollection services, Type implementationType, ServiceLifetime serviceLifetime, Type? serviceType = null, string? key = null)
    {
        if (serviceType is null)
        {
            if (string.IsNullOrEmpty(key))
            {
                services.TryAdd(new ServiceDescriptor(implementationType, implementationType, serviceLifetime));
            }
            else
            {
                services.TryAdd(new ServiceDescriptor(implementationType, key, implementationType, serviceLifetime));
            }

            return services;
        }

        if (!implementationType.IsAssignableTo(serviceType))
        {
            return services;
        }

        if (string.IsNullOrEmpty(key))
        {
            services.TryAdd(
                new ServiceDescriptor(
                    serviceType, implementationType, serviceLifetime));
        }
        else
        {
            services.TryAdd(
                new ServiceDescriptor(
                    implementationType, key, implementationType, serviceLifetime));
        }

        return services;
    }
}
