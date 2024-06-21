using Microsoft.AspNetCore.Mvc;

namespace Peacious.Framework.Results;

public static class ErrorExtension
{
    public static IResult InResult(this Error error) => Result.Failure(error);
    public static IResult<TResponse> InResult<TResponse>(this Error error) => Result.Failure<TResponse>(error);

    public static ObjectResult ToObjectResult(this Error error, ErrorResponseType errorResponseType)
    {
        return errorResponseType switch
        {
            ErrorResponseType.Standard => new ObjectResult(error.ToProblemDetails()),
            ErrorResponseType.OAuth2Error => new ObjectResult(error.ToOAuth2ErrorResponse()),
            _ => new ObjectResult(error) { StatusCode = GetStatusCode(error.Type) },
        };
    }

    public static ProblemDetails ToProblemDetails(this Error error)
    {
        return new ProblemDetails
        {
            Title = error.Title,
            Status = GetStatusCode(error.Type),
            Detail = error.Description,
            Type = error.Uri
        };
    }

    public static OAuth2ErrorResponse ToOAuth2ErrorResponse(this Error error)
    {
        return new OAuth2ErrorResponse
        {
            Error = error.Title,
            Description = error.Description,
            Uri = error.Uri
        };
    }

    private static int GetStatusCode(ErrorType type)
    {
        return type switch
        {
            ErrorType.Validation => 400,
            ErrorType.NotFound => 404,
            ErrorType.Conflict => 409,
            ErrorType.Failure => 500,
            ErrorType.NotImplemented => 501,
            ErrorType.ServiceUnAvailable => 503,
            _ => 500,
        };
    }
}
