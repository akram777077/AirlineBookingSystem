namespace AirlineBookingSystem.Shared.Filters;

public record AirplaneSearchFilter(string? Model, string? Manufacturer) : PaginationFilter;
