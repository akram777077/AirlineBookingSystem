namespace AirlineBookingSystem.Shared.Filters;

public class FlightSearchFilter
{
    public int? FromCityId { get; set; }
    public int? ToCityId { get; set; }
    public int? FromCountryId { get; set; } 
    public int? ToCountryId { get; set; } 
    public DateTimeOffset? DepartureDate { get; set; }
}
