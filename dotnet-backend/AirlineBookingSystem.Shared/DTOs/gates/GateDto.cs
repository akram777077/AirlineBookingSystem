namespace AirlineBookingSystem.Shared.DTOs.Gates;

public struct GateDto
{
    public int Id { get; set; }
    public required string GateNumber { get; set; }
    public int TerminalId { get; set; }
    public required string TerminalName { get; set; }
}
