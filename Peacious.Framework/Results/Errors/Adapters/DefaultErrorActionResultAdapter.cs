using Microsoft.AspNetCore.Mvc;
using Peacious.Framework.Results.Errors.Strategies;

namespace Peacious.Framework.Results.Errors.Adapters;

public class DefaultErrorActionResultAdapter(
    IErrorStatusCodeStrategy errorStatusCodeStrategy) : IErrorActionResultAdapter
{
    private readonly IErrorStatusCodeStrategy _errorStatusCodeStrategy = errorStatusCodeStrategy;

    public IActionResult Convert(Error error)
    {
        return new ObjectResult(error) 
        { 
            StatusCode = _errorStatusCodeStrategy.GetErrorStatusCode(error.Type) 
        };
    }
}
