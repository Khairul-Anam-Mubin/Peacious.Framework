namespace Peacious.Framework.Results.Strategies;

public interface IStatusCodeStrategy
{
    int GetStatusCode(string resultStatus);
}
