using AirlineBookingSystem.Shared.DTOs.terminals;
using AirlineBookingSystem.Shared.Filters;
using AirlineBookingSystem.Shared.Results;
using MediatR;

namespace AirlineBookingSystem.Application.Features.Terminals.Queries.Search;

/// <summary>
/// Represents a query to search for terminals based on a filter.
/// </summary>
/// <param name="Filter">The filter criteria for searching terminals.</param>
public record SearchTerminalsQuery(TerminalSearchFilter Filter) : IRequest<PagedResult<List<TerminalDto>>>;

