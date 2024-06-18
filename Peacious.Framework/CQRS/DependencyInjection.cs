using Microsoft.Extensions.DependencyInjection;
using Peacious.Framework.MessageBrokers;
using System.Reflection;

namespace Peacious.Framework.CQRS;

public static class DependencyInjection
{
    public static IServiceCollection AddCQRS(this IServiceCollection services)
    {
        services.AddTransient<IQueryExecutor, QueryExecutor>();
        services.AddTransient<ICommandExecutor, CommandExecutor>();
        return services;
    }

    public static IServiceCollection AddCQRSWithRabbitMqMassTransit(this IServiceCollection services, MessageBrokerConfig messageBrokerConfig, List<Assembly> assemblies)
    {
        services.AddCQRS();
        services.AddRabbitMqMassTransit(messageBrokerConfig, assemblies);
        return services;    
    }

    public static IServiceCollection AddCQRSWithRabbitMqMassTransit(this IServiceCollection services, MessageBrokerConfig messageBrokerConfig, params Assembly[] assemblies)
    {
        services.AddCQRSWithRabbitMqMassTransit(messageBrokerConfig, assemblies.ToList());
        return services;
    }
}
