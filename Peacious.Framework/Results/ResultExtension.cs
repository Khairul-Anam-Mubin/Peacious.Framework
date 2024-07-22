using Microsoft.AspNetCore.Mvc;
using Peacious.Framework.Results.Adapters;
using Peacious.Framework.Results.Errors.Adapters;

namespace Peacious.Framework.Results;

public static class ResultExtension
{
    public static IResult ToResult<TResponse>(this IResult<TResponse> result) => Result.Create(result);
    public static IResult<TResponse> ToResult<TResponse>(this IResult result) => Result.Create<TResponse>(result);

    public static IActionResult ToActionResult(this IResult result, IActionResultAdapter actionResultAdapter, IErrorActionResultAdapter errorActionResultAdapter)
    {
        return actionResultAdapter.Convert(result, errorActionResultAdapter);
    }
}
