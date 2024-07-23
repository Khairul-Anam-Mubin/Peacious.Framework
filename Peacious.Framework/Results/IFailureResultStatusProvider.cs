namespace Peacious.Framework.Results;

public interface IFailureResultStatusProvider
{
    public string GetFailureStatus(string errorType);
}
