using AirlineBookingSystem.Shared.Filters;

namespace AirlineBookingSystem.Shared.DTOs.Gates;

public class GateSearchFilter : PaginationFilter
{
    public string? GateNumber { get; set; }
    public int? TerminalId { get; set; }
}
