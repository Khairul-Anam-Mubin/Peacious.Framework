namespace Peacious.Framework.MessageBrokers;

internal interface IMessageConsumer<in TMessage> where TMessage : class
{
    Task Consume(IMessageContext<TMessage> context);
}