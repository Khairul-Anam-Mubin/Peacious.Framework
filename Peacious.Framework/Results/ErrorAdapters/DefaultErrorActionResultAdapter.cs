using Microsoft.AspNetCore.Mvc;

namespace Peacious.Framework.Results.ErrorAdapters;

public class DefaultErrorActionResultAdapter : IErrorActionResultAdapter
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
                _instance ??= new DefaultErrorActionResultAdapter();
            }
            return _instance;
        }
    }

    public IActionResult Convert(Error error)
    {
        return new ObjectResult(error) { StatusCode = StatusCodeProvider.GetStatusCode(error.Type) };
    }
}
