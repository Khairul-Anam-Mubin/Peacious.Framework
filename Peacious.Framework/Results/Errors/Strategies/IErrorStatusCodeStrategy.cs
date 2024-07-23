namespace Peacious.Framework.Results.Errors.Strategies;

/// <summary>
/// Converts a errorType to http error code.
/// </summary>
public interface IErrorStatusCodeStrategy
{
    /// <returns>
    /// HttpErrorCode
    /// </returns>
    int GetErrorStatusCode(string errorType);
}
