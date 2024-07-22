using Microsoft.AspNetCore.Mvc;
using Peacious.Framework.Results.Enums;
using Peacious.Framework.Results.ErrorAdapters;
using Peacious.Framework.Results.ErrorFactories;

namespace Peacious.Framework.Results.Extensions;

public static class ErrorExtension
{
    public static IResult Result(this Error error) => Results.Result.Failure(error);
    public static IResult<TResponse> Result<TResponse>(this Error error) => Results.Result.Failure<TResponse>(error);

    public static IActionResult ToActionResult(this Error error, IErrorActionResultAdapter errorActionResultAdapter)
    {
        return errorActionResultAdapter.Convert(error);
    }
}
