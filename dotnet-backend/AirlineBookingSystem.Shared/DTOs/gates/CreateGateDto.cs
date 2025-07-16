namespace AirlineBookingSystem.Shared.DTOs.Gates;

public class CreateGateDto
{
    public string GateNumber { get; set; } = null!;
    public int TerminalId { get; set; }
}
