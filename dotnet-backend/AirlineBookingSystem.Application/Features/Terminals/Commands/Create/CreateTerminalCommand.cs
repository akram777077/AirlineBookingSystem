using AirlineBookingSystem.Shared.DTOs.terminals;
using AirlineBookingSystem.Shared.Results;
using MediatR;

namespace AirlineBookingSystem.Application.Features.Terminals.Commands.Create;

public record CreateTerminalCommand(CreateTerminalDto Dto) : IRequest<Result<int>>;
