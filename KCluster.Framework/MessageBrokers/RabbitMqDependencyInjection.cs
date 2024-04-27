using KCluster.Framework.Extensions;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace KCluster.Framework.MessageBrokers;

public static class RabbitMqDependencyInjection
{
    public static IServiceCollection AddRabbitMqMassTransit(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddTransient<IEventBus, EventBus>();
        services.AddTransient<ICommandBus, CommandBus>();
        services.AddTransient<IMessageRequestClient, MessageRequestClient>();

        services.AddSingleton(configuration.TryGetConfig<MessageBrokerConfig>("MessageBrokerConfig"));
        services.AddMassTransit(busConfigurator =>
        {
            busConfigurator.SetDefaultEndpointNameFormatter();

            AssemblyCache.Instance.GetAddedAssemblies().ForEach(assembly =>
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
