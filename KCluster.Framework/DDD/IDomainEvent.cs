using KCluster.Framework.EDD;

namespace KCluster.Framework.DDD;

public interface IDomainEvent : IEvent
{
    public string Id { get; }
}