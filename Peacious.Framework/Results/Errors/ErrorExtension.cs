using Microsoft.AspNetCore.Mvc;
using Peacious.Framework.Results.Constants;
using Peacious.Framework.Results.Errors.Adapters;

namespace Peacious.Framework.Results.Errors;

public static class ErrorExtension
{
    public static IResult Result(this Error error) => 
        Results.Result.Create(ResultStatus.Error, error);

    public static IResult<TResponse> Result<TResponse>(this Error error) => 
        Results.Result<TResponse>.Create(default, ResultStatus.Error, error);
    
    public static IActionResult ToActionResult(
        this Error error, IErrorActionResultAdapter errorActionResultAdapter) => 
        errorActionResultAdapter.Convert(error);
}
