namespace Peacious.Framework.Results.Errors.Strategies;

public interface IErrorStatusCodeStrategy
{
    int GetErrorStatusCode(string errorType);
}
