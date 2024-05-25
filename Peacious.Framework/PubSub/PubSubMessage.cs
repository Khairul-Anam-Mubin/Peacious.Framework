using Peacious.Framework.CQRS;
using Peacious.Framework.Mediators;

namespace Peacious.Framework.PubSub;

public class PubSubMessage : IRequest, IInternalMessage
{
    public string Id { get; set; } = string.Empty;
    public MessageType MessageType { get; set; }
    public object? Message { get; set; }
    public string? Token { get; set; }
}