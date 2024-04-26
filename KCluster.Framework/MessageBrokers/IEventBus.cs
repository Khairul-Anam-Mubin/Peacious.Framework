namespace KCluster.Framework.MessageBrokers;

public interface IEventBus
{
    Task PublishAsync<TEvent>(TEvent message) where TEvent : class;
}