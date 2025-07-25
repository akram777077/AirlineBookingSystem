using AirlineBookingSystem.Shared.DTOs.terminals;
using AirlineBookingSystem.Shared.Results;
using MediatR;

namespace AirlineBookingSystem.Application.Features.Terminals.Commands.Update;

public record UpdateTerminalCommand(UpdateTerminalDto Dto) : IRequest<Result>;
