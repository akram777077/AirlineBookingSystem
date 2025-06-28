namespace AirlineBookingSystem.Shared.DTOs.Flights;

public class FlightDto
{
    public int Id { get; set; }
    public required string FlightNumber { get; set; }
    public DateTime? DepartureTime { get; set; }
    public DateTime? ArrivalTime { get; set; }
    public required string FromAirportCode { get; set; }
    public required string ToAirportCode { get; set; }
    public required string FromCity { get; set; }
    public required string ToCity { get; set; }
}