namespace AirlineBookingSystem.Shared.DTOs.Seats;

public class GetAvailableSeatsFilterDto
{
    public int FlightId { get; set; }
    public int? ClassTypeId { get; set; }
}