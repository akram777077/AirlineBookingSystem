namespace AirlineBookingSystem.Shared.DTOs.flightClasses;

public struct FlightClassDto
{
    public int Id { get; set; }
    public int FlightId { get; set; }
    public int ClassTypeId { get; set; }
    public decimal Price { get; set; }
    public int TotalSeats { get; set; }
    public int AvailableSeats { get; set; }
}
