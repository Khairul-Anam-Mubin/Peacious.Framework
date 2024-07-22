using Microsoft.AspNetCore.Mvc;
using Peacious.Framework.Results.Enums;
using Peacious.Framework.Results.ErrorAdapters;
using Peacious.Framework.Results.ErrorFactories;
using Peacious.Framework.Results.ResultAdapters;
using Peacious.Framework.Results.ResultFactories;

namespace Peacious.Framework.Results.Extensions;

public static class ResultExtension
{
    public static IResult ToResult<TResponse>(this IResult<TResponse> result) => Result.Create(result);
    public static IResult<TResponse> ToResult<TResponse>(this IResult result) => Result.Create<TResponse>(result);

    public static IActionResult ToActionResult(this IResult result, IActionResultAdapter actionResultAdapter, IErrorActionResultAdapter errorActionResultAdapter)
    {
        return actionResultAdapter.Convert(result, errorActionResultAdapter);
    }
}
