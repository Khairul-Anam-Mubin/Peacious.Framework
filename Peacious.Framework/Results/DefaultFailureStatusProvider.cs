using Peacious.Framework.Results.Constants;

namespace Peacious.Framework.Results;

public class DefaultFailureStatusProvider : IFailureResultStatusProvider
{
    private static readonly object _lockObject = new();
    private static IFailureResultStatusProvider? _instance;

    public static IFailureResultStatusProvider Instance
    {
        get
        {
            if (_instance is not null)
            {
                return _instance;
            }
            lock (_lockObject)
            {
                _instance ??= new DefaultFailureStatusProvider();
            }
            return _instance;
        }
    }

    public string GetFailureStatus(string errorType)
    {
        if (errorType == ErrorType.Failure ||
            errorType == ErrorType.ServiceUnavailable ||
            errorType == ErrorType.NotImplemented)
        {
            return ResultStatus.Failed;
        }

        return ResultStatus.Error;
    }
}
