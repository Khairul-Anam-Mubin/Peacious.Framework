using System.Text.Json.Serialization;

namespace Peacious.Framework.Results;

public record Error
{
    [JsonIgnore]
    public ErrorType Type { get; init; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Title { get; init; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Description { get; init; }
    
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Uri { get; init; }

    private Error(ErrorType type, string? title = null, string? description = null, string? uri = null)
    {
        Type = type;
        Title = title;
        Description = description;
        Uri = uri;
    }

    public static readonly Error None = new Error(ErrorType.None);

    public static Error Validation(string? title = null, string? description = null, string? uri = null)
    {
        return new Error(ErrorType.Validation, title, description, uri);    
    }
    public static Error Unauthorized(string? title = null, string? description = null, string? uri = null)
    {
        return new Error(ErrorType.Unauthorized, title, description, uri);
    }
    public static Error NotFound(string? title = null, string? description = null, string? uri = null)
    {
        return new Error(ErrorType.NotFound, title, description, uri);    
    }
    public static Error Conflict(string? title = null, string? description = null, string? uri = null)
    {
        return new Error(ErrorType.Conflict, title, description, uri);
    }
    public static Error Failure(string? title = null, string? description = null, string? uri = null)
    {
        return new Error(ErrorType.Failure, title, description, uri);
    }
    public static Error ServiceUnAvailable(string? title = null, string? description = null, string? uri = null)
    {
        return new Error(ErrorType.ServiceUnavailable, title, description, uri);
    }
    public static Error NotImplemented(string? title = null, string? description = null, string? uri = null)
    {
        return new Error(ErrorType.NotImplemented, title, description, uri);
    }
}
