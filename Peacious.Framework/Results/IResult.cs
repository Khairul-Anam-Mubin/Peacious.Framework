using Peacious.Framework.Results.Errors;

namespace Peacious.Framework.Results;

public interface IResult
{
    Error Error { get; }
    string? Message { get; }
    string Status { get; }

    bool IsSuccess { get; }
    bool IsFailure { get; }
}

public interface IResult<out TResponse> : IResult
{
    TResponse? Value { get; }
}