using Microsoft.AspNetCore.Mvc;

namespace Peacious.Framework.Results;

public static class ResultExtension
{
    public static IResult ToResult<TResponse>(this IResult<TResponse> result) => Result.Create(result);
    public static IResult<TResponse> ToResult<TResponse>(this IResult result) => Result.Create<TResponse>(result);

    public static ObjectResult ToObjectResult(this IResult result, ErrorResponseType errorResponseType)
    {
        return result.Status switch
        {
            ResponseStatus.Pending => new ObjectResult(result) { StatusCode = 100 },
            ResponseStatus.Processing => new ObjectResult(result) { StatusCode = 102 },
            ResponseStatus.Success => new OkObjectResult(result),
            ResponseStatus.Error or ResponseStatus.Failed => result.Error.ToObjectResult(errorResponseType),
            _ => new ObjectResult(result) { StatusCode = 500 },
        };
    }

    public static ObjectResult ToObjectResult<TResponse>(this IResult<TResponse> result, ErrorResponseType errorResponseType)
    {
        return result.Status switch
        {
            ResponseStatus.Pending => new ObjectResult(result) { StatusCode = 100 },
            ResponseStatus.Processing => new ObjectResult(result) { StatusCode = 102 },
            ResponseStatus.Success => new OkObjectResult(result.Value),
            ResponseStatus.Error or ResponseStatus.Failed => result.Error.ToObjectResult(errorResponseType),
            _ => new ObjectResult(result) { StatusCode = 500 },
        };
    }
}
