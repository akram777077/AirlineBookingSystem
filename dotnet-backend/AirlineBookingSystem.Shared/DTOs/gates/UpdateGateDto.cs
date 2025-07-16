namespace AirlineBookingSystem.Shared.DTOs.Gates;

public class UpdateGateDto
{
    public int Id { get; set; }
    public string GateNumber { get; set; } = null!;
    public int TerminalId { get; set; }
}
