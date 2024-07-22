using Peacious.Framework.Results.Enums;

namespace Peacious.Framework.Results;

public class StatusCodeProvider
{
    public static int GetStatusCode(ErrorType type)
    {
        return type switch
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

    public static int GetStatusCode(ResponseStatus responseStatus)
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
