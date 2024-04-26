using KCluster.Framework.CQRS;

namespace KCluster.Framework.PubSub;

public class PubSubMessage : IInternalMessage
{
    public string Id { get; set; } = string.Empty;
    public MessageType MessageType { get; set; }
    public object? Message { get; set; }
    public string? Token { get; set; }
}