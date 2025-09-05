namespace AirlineBookingSystem.Shared.Results.Error;

/// <summary>
/// Represents an error result data transfer object.
/// </summary>
public class ErrorResultDto
{
    /// <summary>
    /// Represents an error item.
    /// </summary>
    public class ErrorItem
    {
        /// <summary>
        /// Gets or sets the error message.
        /// </summary>
        public string Error { get; set; } = string.Empty;
    }

    /// <summary>
    /// Gets or sets the error message.
    /// </summary>
    public string Message { get; set; } = string.Empty;
    /// <summary>
    /// Gets or sets the list of errors.
    /// </summary>
    public List<ErrorItem> Errors { get; set; } = new();
}
