using AirlineBookingSystem.Shared.Filters;

namespace AirlineBookingSystem.Shared.DTOs.Gates;

public record GateSearchFilter(string? GateNumber, int? TerminalId) : PaginationFilter;
