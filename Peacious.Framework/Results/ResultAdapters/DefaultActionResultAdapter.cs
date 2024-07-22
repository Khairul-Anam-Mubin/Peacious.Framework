using Microsoft.AspNetCore.Mvc;
using Peacious.Framework.Results.ErrorAdapters;

namespace Peacious.Framework.Results.ResultAdapters;

public class DefaultActionResultAdapter : IActionResultAdapter
{
    private static readonly object _lockObject = new();
    private static IActionResultAdapter? _instance;

    public static IActionResultAdapter Instance
    {
        get
        {
            if (_instance is not null)
            {
                return _instance;
            }
            lock (_lockObject)
            {
                _instance ??= new DefaultActionResultAdapter();
            }
            return _instance;
        }
    }

    public IActionResult Convert(IResult result, IErrorActionResultAdapter visitor)
    {
        var statusCode = StatusCodeProvider.GetStatusCode(result.Status);

        if (statusCode == 500 || result.Error != Error.None)
        {
            return visitor.Convert(result.Error);
        }

        return new ObjectResult(result) { StatusCode = statusCode };
    }
}
