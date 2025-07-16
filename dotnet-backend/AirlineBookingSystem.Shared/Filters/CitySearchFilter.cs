using AirlineBookingSystem.Shared.Filters;

namespace AirlineBookingSystem.Shared.DTOs.Cities;

public class CitySearchFilter : PaginationFilter
{
    public int? CountryId { get; set; }
    public string? Name { get; set; }
}
