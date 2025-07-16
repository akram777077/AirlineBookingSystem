using AirlineBookingSystem.Shared.DTOs.terminals;
using AirlineBookingSystem.Shared.Filters;
using AirlineBookingSystem.Shared.Results;
using MediatR;
using System.Collections.Generic;

namespace AirlineBookingSystem.Application.Features.Terminals.Queries.SearchTerminals;

public record SearchTerminalsQuery(TerminalSearchFilter Filter) : IRequest<PagedResult<List<TerminalDto>>>;
