using Microsoft.AspNetCore.Mvc;

namespace Peacious.Framework.Results.ErrorAdapters;

public class ProblemDetailsErrorActionResultAdapter : IErrorActionResultAdapter
{
    private static readonly object _lockObject = new();
    private static IErrorActionResultAdapter? _instance;

    public static IErrorActionResultAdapter Instance
    {
        get
        {
            if (_instance is not null)
            {
                return _instance;
            }
            lock (_lockObject)
            {
                _instance ??= new ProblemDetailsErrorActionResultAdapter();
            }
            return _instance;
        }
    }

    public IActionResult Convert(Error error)
    {
        var statusCode = StatusCodeProvider.GetStatusCode(error.Type);

        var problemDetails = new ProblemDetails
        {
            Title = error.Title,
            Status = statusCode,
            Detail = error.Description,
            Type = error.Uri
        };

        return new ObjectResult(problemDetails)
        {
            StatusCode = statusCode
        };
    }
}
