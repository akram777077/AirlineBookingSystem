namespace AirlineBookingSystem.Shared.DTOs.FlightClass;

public class FlightClassDto
{
    public int Id { get; set; }
    public required int FlightId { get; set; }
    public int ClassId { get; set; }
    public decimal Price { get; set; }
    public int Seats { get; set; }
}
