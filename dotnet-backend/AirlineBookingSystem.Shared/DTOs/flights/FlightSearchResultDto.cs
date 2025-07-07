namespace AirlineBookingSystem.Shared.DTOs.flights;

public class FlightSearchResultDto
{
    public class FlightSegmentSearchDto
    {
        public required string City { get; set; }
        public required string Country { get; set; }
        public required string AirportCode { get; set; }
        public DateTimeOffset? Time { get; set; }
        
    }
    public int Id { get; set; }
    public required string FlightNumber { get; set; }
    public required string Airline { get; set; }
    public required string Status { get; set; }
    public required FlightSegmentSearchDto Departure { get; set; }
    public required FlightSegmentSearchDto Arrival { get; set; }
}
