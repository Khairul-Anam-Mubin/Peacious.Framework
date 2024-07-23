using Microsoft.AspNetCore.Mvc;
using Peacious.Framework.Results.Errors.Adapters;
using Peacious.Framework.Results.Strategies;

namespace Peacious.Framework.Results.Adapters;

public class DefaultActionResultAdapter(
    IStatusCodeStrategy statusCodeStrategy) : IActionResultAdapter
{
    private readonly IStatusCodeStrategy _statusCodeStrategy = statusCodeStrategy;

    public IActionResult Convert(IResult result, IErrorActionResultAdapter visitor)
    {
        if (result.IsFailure)
        {
            return visitor.Convert(result.Error);
        }

        return new ObjectResult(result) 
        { 
            StatusCode = _statusCodeStrategy.GetStatusCode(result.Status )
        };
    }
}
