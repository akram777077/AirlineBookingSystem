namespace AirlineBookingSystem.Shared.DTOs.Gates;

public struct UpdateGateDto
{
    public int Id { get; set; }
    public required string GateNumber { get; set; }
    public int TerminalId { get; set; }
}
