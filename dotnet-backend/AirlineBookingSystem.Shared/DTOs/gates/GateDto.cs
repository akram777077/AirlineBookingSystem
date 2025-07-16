namespace AirlineBookingSystem.Shared.DTOs.Gates;

public class GateDto
{
    public int Id { get; set; }
    public string GateNumber { get; set; } = null!;
    public int TerminalId { get; set; }
    public string TerminalName { get; set; } = null!;
}
