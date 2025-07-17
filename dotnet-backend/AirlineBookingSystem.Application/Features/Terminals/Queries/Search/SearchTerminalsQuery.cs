using AirlineBookingSystem.Shared.DTOs.terminals;
using AirlineBookingSystem.Shared.Filters;
using AirlineBookingSystem.Shared.Results;
using MediatR;

namespace AirlineBookingSystem.Application.Features.Terminals.Queries.Search;

public record SearchTerminalsQuery(TerminalSearchFilter Filter) : IRequest<PagedResult<List<TerminalDto>>>;
