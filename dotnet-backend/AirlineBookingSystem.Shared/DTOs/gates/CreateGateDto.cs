namespace AirlineBookingSystem.Shared.DTOs.Gates;

public struct CreateGateDto
{
    public required string GateNumber { get; set; }
    public int TerminalId { get; set; }
}
