namespace AirlineBookingSystem.Shared.Filters;

public class TerminalSearchFilter : PaginationFilter
{
    public int? AirportId { get; set; }
    public string? Name { get; set; }
}
