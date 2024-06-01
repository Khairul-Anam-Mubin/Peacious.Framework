using Microsoft.Extensions.DependencyInjection;
using Peacious.Framework.Mediators;
using Peacious.Framework.MessageBrokers;
using System.Reflection;

namespace Peacious.Framework.CQRS;

public static class DependencyInjection
{
    public static IServiceCollection AddCQRS(this IServiceCollection services, List<Assembly> assemblies)
    {
        services.AddMediator(assemblies);
        services.AddTransient<IQueryExecutor, QueryExecutor>();
        services.AddTransient<ICommandExecutor, CommandExecutor>();
        return services;
    }

    public static IServiceCollection AddCQRS(this IServiceCollection services, params Assembly[] assemblies)
    {
        return services.AddCQRS(assemblies.ToList());
    }

    public static IServiceCollection AddCQRSWithRabbitMqMassTransit(this IServiceCollection services, MessageBrokerConfig messageBrokerConfig, List<Assembly> assemblies)
    {
        services.AddCQRS(assemblies);
        services.AddRabbitMqMassTransit(messageBrokerConfig, assemblies);
        services.AddTransient<ICommandService, CommandService>();
        services.AddTransient<IQueryService, QueryService>();
        return services;    
    }

    public static IServiceCollection AddCQRSWithRabbitMqMassTransit(this IServiceCollection services, MessageBrokerConfig messageBrokerConfig, params Assembly[] assemblies)
    {
        services.AddCQRSWithRabbitMqMassTransit(messageBrokerConfig, assemblies.ToList());
        return services;
    }
}
