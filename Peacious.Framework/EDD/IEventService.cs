using Peacious.Framework.CQRS;

namespace Peacious.Framework.EDD;

public interface IEventService
{
    Task PublishIntegrationEventAsync<TEvent>(TEvent @event)
        where TEvent : class, IEvent, IInternalMessage;

    Task PublishEventAsync<TEvent>(TEvent @event)
        where TEvent : class, IEvent;
}