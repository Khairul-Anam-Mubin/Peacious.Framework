using Peacious.Framework.Results.Enums;

namespace Peacious.Framework.Results.Errors.Strategies;

public interface IErrorStatusCodeStrategy
{
    int GetErrorStatusCode(ErrorType errorType);
}
