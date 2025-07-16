namespace AirlineBookingSystem.Shared.Filters;

public class AirplaneSearchFilter : PaginationFilter
{
    public string? Model { get; set; }
    public string? Manufacturer { get; set; }
}
