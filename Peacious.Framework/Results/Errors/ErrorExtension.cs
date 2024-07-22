using Microsoft.AspNetCore.Mvc;
using Peacious.Framework.Results.Errors.Adapters;

namespace Peacious.Framework.Results.Errors;

public static class ErrorExtension
{
    public static IResult Result(this Error error) => Results.Result.Failure(error);
    public static IResult<TResponse> Result<TResponse>(this Error error) => Results.Result.Failure<TResponse>(error);

    public static IActionResult ToActionResult(this Error error, IErrorActionResultAdapter errorActionResultAdapter)
    {
        return errorActionResultAdapter.Convert(error);
    }
}
