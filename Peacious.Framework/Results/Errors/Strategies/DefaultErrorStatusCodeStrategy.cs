using Peacious.Framework.Results.Constants;

namespace Peacious.Framework.Results.Errors.Strategies;

public class DefaultErrorStatusCodeStrategy : IErrorStatusCodeStrategy
{
    private static readonly object _lockObject = new();
    private static IErrorStatusCodeStrategy? _instance;

    public static IErrorStatusCodeStrategy Instance
    {
        get
        {
            if (_instance is not null)
            {
                return _instance;
            }
            lock (_lockObject)
            {
                _instance ??= new DefaultErrorStatusCodeStrategy();
            }
            return _instance;
        }
    }

    public int GetErrorStatusCode(string errorType)
    {
        return errorType switch
        {
            ErrorType.Validation => 400,
            ErrorType.Unauthorized => 401,
            ErrorType.NotFound => 404,
            ErrorType.Conflict => 409,
            ErrorType.Failure => 500,
            ErrorType.NotImplemented => 501,
            ErrorType.ServiceUnavailable => 503,
            _ => 500,
        };
    }
}