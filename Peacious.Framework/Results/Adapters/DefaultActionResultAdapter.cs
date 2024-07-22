using Microsoft.AspNetCore.Mvc;
using Peacious.Framework.Results.Errors;
using Peacious.Framework.Results.Errors.Adapters;

namespace Peacious.Framework.Results.Adapters;

public class DefaultActionResultAdapter : IActionResultAdapter
{
    #region SingletonInstanceCreation
    
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
    
    #endregion
    
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
