using AirlineBookingSystem.Shared.DTOs.terminals;
using AirlineBookingSystem.Shared.Results;
using MediatR;

namespace AirlineBookingSystem.Application.Features.Terminals.Queries.GetById;

public record GetTerminalByIdQuery(int Id) : IRequest<Result<TerminalDto>>;
