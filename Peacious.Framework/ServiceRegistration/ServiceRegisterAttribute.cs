using Microsoft.Extensions.DependencyInjection;

namespace Peacious.Framework.ServiceRegistration;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
public class ServiceRegisterAttribute : Attribute
{
    public string? Key { get; private set; }
    public Type? ServiceType { get; private set; }
    public ServiceLifetime ServiceLifetime { get; private set; }

    public ServiceRegisterAttribute(ServiceLifetime serviceLifetime)
    {
        ServiceLifetime = serviceLifetime;
    }

    public ServiceRegisterAttribute(ServiceLifetime serviceLifetime, Type serviceType)
    {
        ServiceLifetime = serviceLifetime;
        ServiceType = serviceType;
    }

    public ServiceRegisterAttribute(ServiceLifetime serviceLifetime, string key)
    {
        ServiceLifetime = serviceLifetime;
        Key = key;
    }

    public ServiceRegisterAttribute(ServiceLifetime serviceLifetime, Type serviceType, string key)
    {
        ServiceLifetime = serviceLifetime;
        ServiceType = serviceType;
        Key = key;
    }
}