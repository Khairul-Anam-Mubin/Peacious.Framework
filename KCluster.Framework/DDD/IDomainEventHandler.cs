using KCluster.Framework.EDD;

namespace KCluster.Framework.DDD;

public interface IDomainEventHandler<TDomainEvent> : IEventHandler<TDomainEvent>
    where TDomainEvent : class, IDomainEvent
{ }
