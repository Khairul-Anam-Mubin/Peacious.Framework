namespace Peacious.Framework.CQRS;

public interface IInternalMessage
{
    string? Token { get; set; }
}