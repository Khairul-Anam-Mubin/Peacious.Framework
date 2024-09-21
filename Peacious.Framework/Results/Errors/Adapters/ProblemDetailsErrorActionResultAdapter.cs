using Microsoft.AspNetCore.Mvc;
using Peacious.Framework.Results.Errors.Strategies;

namespace Peacious.Framework.Results.Errors.Adapters;

public class ProblemDetailsErrorActionResultAdapter(
    IErrorStatusCodeStrategy errorStatusCodeStrategy) : IErrorActionResultAdapter
{
    private readonly IErrorStatusCodeStrategy _errorStatusCodeStrategy = errorStatusCodeStrategy;

    public IActionResult Convert(Error error)
    {
        var statusCode = _errorStatusCodeStrategy.GetErrorStatusCode(error.Type);

        var problemDetails = new ProblemDetails
        {
            Title = error.Type,
            Status = statusCode,
            Detail = error.Message
        };

        return new ObjectResult(problemDetails)
        {
            StatusCode = statusCode
        };
    }
}
