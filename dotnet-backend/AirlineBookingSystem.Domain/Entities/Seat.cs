namespace AirlineBookingSystem.Domain.Entities;

public class Seat
{
    public int Id { get; set; }
    public int ClassTypesId { get; set; }
    public required string SeatNumber { get; set; }
    public bool IsReserved { get; set; } = false;
    public int AirplaneId { get; set; }

    public required ClassType ClassType { get; set; }
    public required Airplane Airplane { get; set; }
    public Booking? Booking { get; set; }
}
