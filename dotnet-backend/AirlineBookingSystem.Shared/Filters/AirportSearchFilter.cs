using AirlineBookingSystem.Shared.Filters;

namespace AirlineBookingSystem.Shared.Filters;

public class AirportSearchFilter : PaginationFilter
{
    public string? AirportCode { get; set; }
    public string? Name { get; set; }
    public int? CityId { get; set; }
}