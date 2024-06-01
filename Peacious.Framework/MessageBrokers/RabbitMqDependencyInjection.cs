using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Peacious.Framework.MessageBrokers;

public static class RabbitMqDependencyInjection
{
    public static IServiceCollection AddRabbitMqMassTransit(this IServiceCollection services, MessageBrokerConfig messageBrokerConfig, List<Assembly> assemblies)
    {
        services.AddTransient<IEventBus, EventBus>();
        services.AddTransient<ICommandBus, CommandBus>();
        services.AddTransient<IMessageRequestClient, MessageRequestClient>();

        services.AddSingleton(messageBrokerConfig);
        
        services.AddMassTransit(busConfigurator =>
        {
            busConfigurator.SetDefaultEndpointNameFormatter();

            assemblies.ForEach(assembly =>
                busConfigurator.AddConsumers(assembly));

            busConfigurator.UsingRabbitMq((context, configurator) =>
            {
                var messageBrokerConfig = context.GetRequiredService<MessageBrokerConfig>();

                configurator.Host(new Uri(messageBrokerConfig.Host), hostConfigurator =>
                {
                    hostConfigurator.Username(messageBrokerConfig.UserName);
                    hostConfigurator.Password(messageBrokerConfig.Password);
                });

                configurator.AutoDelete = true;

                configurator.ConfigureEndpoints(context);
            });
        });

        return services;
    }
}
