using Peacious.Framework.CQRS;
using Peacious.Framework.IdentityScope;
using Peacious.Framework.MessageBrokers;

namespace Peacious.Framework.EDD;

public class EventService : IEventService
{
    private readonly IEventBus _eventBus;
    private readonly IIdentityScopeContext _identityScopeContext;
    private readonly IEventExecutor _eventExecutor;

    public EventService(
        IEventBus eventBus,
        IIdentityScopeContext scopeIdentity,
        IEventExecutor eventExecutor)
    {
        _eventBus = eventBus;
        _identityScopeContext = scopeIdentity;
        _eventExecutor = eventExecutor;
    }

    public async Task PublishIntegrationEventAsync<TEvent>(TEvent @event)
        where TEvent : class, IEvent, IInternalMessage
    {
        @event.Token = _identityScopeContext.Token;
        await _eventBus.PublishAsync(@event);
    }

    public async Task PublishEventAsync<TEvent>(TEvent @event)
        where TEvent : class, IEvent
    {
        await _eventExecutor.PublishAsync(@event);
    }
}
