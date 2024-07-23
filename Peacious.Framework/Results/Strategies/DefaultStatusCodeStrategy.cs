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

    public int GetStatusCode(string resultStatus)
    {
        return resultStatus switch
        {
            ResultStatus.Pending => 100,
            ResultStatus.Processing => 102,
            ResultStatus.Success => 200,
            _ => throw new Exception($"DefaultStatusCodeStrategy failed to convert the result status : {resultStatus} to status code")
        };
    }
}
