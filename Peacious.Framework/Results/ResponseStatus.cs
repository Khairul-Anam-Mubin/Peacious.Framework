namespace Peacious.Framework.Results;

public enum ResponseStatus
{
    /// <summary>
    /// Request acknowledged and queued for processing.
    /// </summary>
    Pending = 0,
    /// <summary>
    /// Long running task to process. Client should do polling.
    /// </summary>
    Processing = 1,
    /// <summary>
    /// Respond successfully 
    /// </summary>
    Success = 2,
    /// <summary>
    /// Authorizaiton error, Invalid inputs, validation or violation of business rules error.
    /// </summary>
    Error = 4,
    /// <summary>
    /// Server error, any network, db call failure or something bad happens.
    /// </summary>
    Failed = 5
}