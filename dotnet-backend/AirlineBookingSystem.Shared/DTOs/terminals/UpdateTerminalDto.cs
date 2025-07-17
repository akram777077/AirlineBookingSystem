namespace AirlineBookingSystem.Shared.DTOs.terminals;

public struct UpdateTerminalDto
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public int AirportId { get; set; } 
    
}
