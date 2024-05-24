using MassTransit;

namespace Peacious.Framework.MessageBrokers;

public class EventBus : IEventBus
{
    private readonly IPublishEndpoint _publishEndpoint;

    public EventBus(IPublishEndpoint publishEndpoint)
    {
        _publishEndpoint = publishEndpoint;
    }

    public async Task PublishAsync<TEvent>(TEvent message) where TEvent : class
    {
        await _publishEndpoint.Publish(message);
    }
}