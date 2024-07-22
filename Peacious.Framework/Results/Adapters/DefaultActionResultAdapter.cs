using Microsoft.AspNetCore.Mvc;
using Peacious.Framework.Results.Errors;
using Peacious.Framework.Results.Errors.Adapters;
using Peacious.Framework.Results.Strategies;

namespace Peacious.Framework.Results.Adapters;

public class DefaultActionResultAdapter(
    IStatusCodeStrategy statusCodeStrategy) : IActionResultAdapter
{
    private readonly IStatusCodeStrategy _statusCodeStrategy = statusCodeStrategy;

    public IActionResult Convert(IResult result, IErrorActionResultAdapter visitor)
    {
        var statusCode = _statusCodeStrategy.GetStatusCode(result.Status);

        if (statusCode == 500 || result.Error != Error.None || result.IsFailure)
        {
            return visitor.Convert(result.Error);
        }

        return new ObjectResult(result) { StatusCode = statusCode };
    }
}
