using Peacious.Framework.EDD;

namespace Peacious.Framework.DDD;

public interface IDomainEventHandler<TDomainEvent> : IEventHandler<TDomainEvent>
    where TDomainEvent : class, IDomainEvent
{ }
