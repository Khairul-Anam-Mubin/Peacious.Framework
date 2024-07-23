using System.Text.Json.Serialization;
using Peacious.Framework.Results.Constants;

namespace Peacious.Framework.Results.Errors;

public record Error
{
    [JsonIgnore]
    public string Type { get; init; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Title { get; init; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Description { get; init; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Uri { get; init; }

    protected Error(string type, string? title = null, string? description = null, string? uri = null)
    {
        Type = type;
        Title = title;
        Description = description;
        Uri = uri;
    }

    public static Error Create(string type, string? title = null, string? description = null, string? uri = null)
    {
        return new Error(type, title, description, uri);
    }

    /// <summary>
    /// Null object pattern
    /// </summary>
    public static readonly Error None = Create(ErrorType.None);

    #region DefaultErrors

    public static Error Validation(string? title = null, string? description = null, string? uri = null)
    {
        return Create(ErrorType.Validation, title, description, uri);
    }
    public static Error Unauthorized(string? title = null, string? description = null, string? uri = null)
    {
        return Create(ErrorType.Unauthorized, title, description, uri);
    }
    public static Error NotFound(string? title = null, string? description = null, string? uri = null)
    {
        return Create(ErrorType.NotFound, title, description, uri);
    }
    public static Error Conflict(string? title = null, string? description = null, string? uri = null)
    {
        return Create(ErrorType.Conflict, title, description, uri);
    }
    public static Error Failure(string? title = null, string? description = null, string? uri = null)
    {
        return Create(ErrorType.Failure, title, description, uri);
    }
    public static Error ServiceUnAvailable(string? title = null, string? description = null, string? uri = null)
    {
        return Create(ErrorType.ServiceUnavailable, title, description, uri);
    }
    public static Error NotImplemented(string? title = null, string? description = null, string? uri = null)
    {
        return Create(ErrorType.NotImplemented, title, description, uri);
    }

    #endregion
}
