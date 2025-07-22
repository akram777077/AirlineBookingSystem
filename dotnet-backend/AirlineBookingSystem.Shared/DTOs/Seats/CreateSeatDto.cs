namespace AirlineBookingSystem.Shared.DTOs;

public class CreateSeatDto
{
    public int ClassTypesId { get; set; }
    public required string SeatNumber { get; set; }
    public bool IsReserved { get; set; }
    public int AirplaneId { get; set; }
}