using AirlineBookingSystem.Shared.Filters;

namespace AirlineBookingSystem.Shared.Filters;

public record AirportSearchFilter(string? AirportCode, string? Name, int? CityId) : PaginationFilter;