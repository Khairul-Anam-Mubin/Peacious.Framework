using Peacious.Framework.Results.Constants;

namespace Peacious.Framework.Results.Errors;

public record Error
{
    /// <summary>
    /// Validation, Authorization, NotFound etc. Can be used to uniquely identify an error.
    /// </summary>
    public string Type { get; init; }
    /// <summary>
    /// Human readable error description / message
    /// </summary>
    public string? Message { get; init; }

    protected Error(string type, string? message)
    {
        Type = type;
        Message = message;
    }

    public static Error Create(string type, string? message = null)
    {
        return new Error(type, message);
    }

    public static readonly Error None = Create(ErrorType.None);

    #region DefaultErrors

    public static Error Validation(string? message)
    {
        return Create(ErrorType.Validation, message);
    }
    public static Error Unauthorized(string? message)
    {
        return Create(ErrorType.Unauthorized, message);
    }
    public static Error NotFound(string? message)
    {
        return Create(ErrorType.NotFound, message);
    }
    public static Error Conflict(string? message)
    {
        return Create(ErrorType.Conflict, message);
    }
    public static Error Failure(string? message)
    {
        return Create(ErrorType.Failure, message);
    }
    public static Error ServiceUnAvailable(string? message)
    {
        return Create(ErrorType.ServiceUnavailable, message);
    }
    public static Error NotImplemented(string? message)
    {
        return Create(ErrorType.NotImplemented, message);
    }

    #endregion
}
