using Peacious.Framework.Results.Constants;

namespace Peacious.Framework.Results.Strategies;

public class DefaultStatusCodeStrategy : IStatusCodeStrategy
{
    private static readonly object _lockObject = new();
    private static IStatusCodeStrategy? _instance;

    public static IStatusCodeStrategy Instance
    {
        get
        {
            if (_instance is not null)
            {
                return _instance;
            }
            lock (_lockObject)
            {
                _instance ??= new DefaultStatusCodeStrategy();
            }
            return _instance;
        }
    }

    public int GetStatusCode(string responseStatus)
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
