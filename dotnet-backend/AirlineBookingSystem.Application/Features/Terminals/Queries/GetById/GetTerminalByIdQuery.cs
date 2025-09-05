using AirlineBookingSystem.Shared.DTOs.terminals;
using AirlineBookingSystem.Shared.Results;
using MediatR;

namespace AirlineBookingSystem.Application.Features.Terminals.Queries.GetById;

/// <summary>
/// Represents a query to retrieve a terminal by its unique identifier.
/// </summary>
/// <param name="Id">The unique identifier of the terminal.</param>
public record GetTerminalByIdQuery(int Id) : IRequest<Result<TerminalDto>>;

