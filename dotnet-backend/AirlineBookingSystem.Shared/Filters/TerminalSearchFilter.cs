namespace AirlineBookingSystem.Shared.Filters;

public record TerminalSearchFilter(int? AirportId, string? Name) : PaginationFilter;
