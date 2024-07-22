using Peacious.Framework.Results.Enums;

namespace Peacious.Framework.Results.Strategies;

public interface IStatusCodeStrategy
{
    int GetStatusCode(ResponseStatus responseStatus);
}
