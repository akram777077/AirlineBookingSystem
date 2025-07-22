namespace AirlineBookingSystem.Shared.DTOs;

public class SeatDto
{
    public int Id { get; set; }
    public int ClassTypesId { get; set; }
    public required string SeatNumber { get; set; }
    public bool IsReserved { get; set; }
    public int AirplaneId { get; set; }
}