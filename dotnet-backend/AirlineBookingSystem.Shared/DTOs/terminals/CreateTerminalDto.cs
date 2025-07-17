namespace AirlineBookingSystem.Shared.DTOs.terminals;

public struct CreateTerminalDto
{
    public required string Name { get; set; } 
    public int AirportId { get; set; } 
}
