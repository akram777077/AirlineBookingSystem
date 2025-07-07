namespace AirlineBookingSystem.Shared.Results.Error;

public class ErrorResultDto
{
    public string Message { get; set; } = "Validation failed";
    public List<ErrorItem> Errors { get; set; } = new();

    public class ErrorItem
    {
        public string Error { get; set; } = string.Empty;
    }
}
