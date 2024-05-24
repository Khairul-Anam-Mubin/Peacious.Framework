using Peacious.Framework.EDD;

namespace Peacious.Framework.DDD;

public interface IDomainEvent : IEvent
{
    public string Id { get; }
}