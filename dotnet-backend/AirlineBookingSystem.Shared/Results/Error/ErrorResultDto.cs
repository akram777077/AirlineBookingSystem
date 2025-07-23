namespace AirlineBookingSystem.Shared.Results.Error;

public record ErrorResultDto(string Message = "Validation failed", List<ErrorItem>? Errors = null);

public record ErrorItem(string Error);
