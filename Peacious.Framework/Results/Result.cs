using System.Text.Json.Serialization;
using Peacious.Framework.Results.Constants;
using Peacious.Framework.Results.Errors;

namespace Peacious.Framework.Results;

public class Result : IResult
{
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public string? Message { get; private set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public Error Error { get; private set; }

    public string Status { get; private set; }

    [JsonIgnore]
    public bool IsSuccess => Error is null || Error == Error.None;

    [JsonIgnore]
    public bool IsFailure => !IsSuccess;

    protected Result(string status, Error error, string? message = null)
    {
        Message = message;
        Status = status;
        Error = error;
    }

    public static IResult Processing(string? message = null) => new Result(ResponseStatus.Processing, Error.None);
    public static IResult Pending(string? message = null) => new Result(ResponseStatus.Pending, Error.None);
    public static IResult Success(string? message = null) => new Result(ResponseStatus.Success, Error.None);
    
    public static IResult Failure(Error error)
    {
        ArgumentNullException.ThrowIfNull(error);

        return new Result(GetFailureResponseStatus(error), error);
    }

    public static IResult<TResponse> Failure<TResponse>(Error error)
    {
        ArgumentNullException.ThrowIfNull(error);

        return Result<TResponse>.Create(GetFailureResponseStatus(error), default, error, null);
    }

    public static IResult<TResponse> Success<TResponse>(TResponse? response, string? message = null)
        => Result<TResponse>.Create(ResponseStatus.Success, response, Error.None ,message);

    public static IResult<TResponse> Success<TResponse>(string? message = null)
        => Result<TResponse>.Create(ResponseStatus.Success, default, Error.None, message);
    
    public static IResult<TResponse> Create<TResponse>(IResult result)
        => Result<TResponse>.Create(result.Status, default, result.Error, result.Message);

    public static IResult Create<TResponse>(IResult<TResponse> result)
        => new Result(result.Status, result.Error, result.Message);

    private static string GetFailureResponseStatus(Error error)
    {
        if (error.Type == ErrorType.Failure || 
            error.Type == ErrorType.ServiceUnavailable || 
            error.Type == ErrorType.NotImplemented)
        {
            return ResponseStatus.Failed;
        }

        return ResponseStatus.Error;
    }
}

public class Result<TResponse> : Result, IResult<TResponse>
{
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public TResponse? Value { get; }

    private Result(string status, TResponse? value, Error error, string? message = null) 
        : base(status, error, message)
    {
        Value = value;
    }

    internal static IResult<TResponse> Create(string status, TResponse? value, Error error, string? message = null)
        => new Result<TResponse>(status, value, error, message);
}