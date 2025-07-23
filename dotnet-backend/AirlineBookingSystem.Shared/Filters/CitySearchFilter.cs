using AirlineBookingSystem.Shared.Filters;

namespace AirlineBookingSystem.Shared.DTOs.Cities;

public record CitySearchFilter(int? CountryId, string? Name) : PaginationFilter;
