namespace AirlineBookingSystem.Shared.Filters;

public record FlightSearchFilter(int? FromCityId, int? ToCityId, int? FromCountryId, int? ToCountryId, DateTimeOffset? DepartureDate) : PaginationFilter;
