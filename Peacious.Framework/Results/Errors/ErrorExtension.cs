using Microsoft.AspNetCore.Mvc;
using Peacious.Framework.Results.Errors.Adapters;

namespace Peacious.Framework.Results.Errors;

public static class ErrorExtension
{
    public static IResult Result(this Error error)
        => error.Result(DefaultFailureStatusProvider.Instance);

    public static IResult<TResponse> Result<TResponse>(
        this Error error)
        => error.Result<TResponse>(DefaultFailureStatusProvider.Instance);

    public static IResult Result(this Error error, IFailureResultStatusProvider failureResultStatusProvider) 
        => Results.Result.Create(failureResultStatusProvider.GetFailureStatus(error.Type), error);
    
    public static IResult<TResponse> Result<TResponse>(
        this Error error, IFailureResultStatusProvider failureResultStatusProvider) 
        => Results.Result<TResponse>.Create(default, failureResultStatusProvider.GetFailureStatus(error.Type), error);
    
    public static IActionResult ToActionResult(
        this Error error, IErrorActionResultAdapter errorActionResultAdapter) => errorActionResultAdapter.Convert(error);
}
