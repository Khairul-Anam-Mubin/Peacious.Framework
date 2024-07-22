namespace Peacious.Framework.Results.Constants;

public class ResponseStatus
{
    /// <summary>
    /// Request acknowledged and queued for processing.
    /// </summary>
    public const string Pending = "Pending";
    /// <summary>
    /// Long running task to process. Client should do polling.
    /// </summary>
    public const string Processing = "Processing";
    /// <summary>
    /// Respond successfully 
    /// </summary>
    public const string Success = "Success";
    /// <summary>
    /// Authorizaiton error, Invalid inputs, validation or violation of business rules error.
    /// </summary>
    public const string Error = "Error";
    /// <summary>
    /// Server error, any network, db call failure or something bad happens.
    /// </summary>
    public const string Failed = "Failed";
}