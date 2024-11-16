using System.Text.Json.Serialization;
using Peacious.Framework.Results.Constants;
using Peacious.Framework.Results.Errors;

namespace Peacious.Framework.Results;

public class Result : IResult
{
    public string Status { get; init; }
    public Error Error { get; init; }
    public string? Message { get; init; }

    [JsonIgnore]
    public bool IsSuccess => Error is null || Error == Error.None;

    [JsonIgnore]
    public bool IsFailure => !IsSuccess;

    protected Result(string status, Error error, string? message = null)
    {
        Status = status;
        Error = error;
        Message = message;
    }

    public static IResult Create(string status, Error error, string? message = null) 
        => new Result(status, error, message);

    #region DefaultResults

    public static IResult Pending(string? message = null)
    => Create(ResultStatus.Pending, Error.None);

    public static IResult Processing(string? message = null)
        => Create(ResultStatus.Processing, Error.None);

    public static IResult Success(string? message = null)
        => Create(ResultStatus.Success, Error.None);

    public static IResult<TResponse> Success<TResponse>(TResponse? response, string? message = null)
        => Result<TResponse>.Create(response, ResultStatus.Success, Error.None, message);
    
    #endregion
}

public class Result<TResponse> : Result, IResult<TResponse>
{
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public TResponse? Value { get; }

    protected Result(TResponse? value, string status, Error error, string? message = null) 
        : base(status, error, message)
    {
        Value = value;
    }

    public static IResult<TResponse> Create(TResponse? value, string status, Error error, string? message = null)
        => new Result<TResponse>(value, status, error, message);
}