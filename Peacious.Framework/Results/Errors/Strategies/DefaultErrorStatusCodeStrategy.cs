using Peacious.Framework.Results.Enums;

namespace Peacious.Framework.Results.Errors.Strategies;

public class DefaultErrorStatusCodeStrategy : IErrorStatusCodeStrategy
{
    public int GetErrorStatusCode(ErrorType errorType)
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