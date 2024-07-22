using Peacious.Framework.Results.Enums;

namespace Peacious.Framework.Results.Strategies;

public class DefaultStatusCodeStrategy : IStatusCodeStrategy
{
    public int GetStatusCode(ResponseStatus responseStatus)
    {
        return responseStatus switch
        {
            ResponseStatus.Pending => 100,
            ResponseStatus.Processing => 102,
            ResponseStatus.Success => 200,
            _ => 500
        };
    }
}
