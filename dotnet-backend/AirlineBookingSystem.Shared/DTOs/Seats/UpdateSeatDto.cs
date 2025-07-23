namespace AirlineBookingSystem.Shared.DTOs.Seats;

public record UpdateSeatDto
{
    public int ClassTypesId { get; set; }
    public required string SeatNumber { get; set; }
    public bool IsReserved { get; set; }
    public int AirplaneId { get; set; }
}