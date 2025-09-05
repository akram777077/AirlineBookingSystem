using AirlineBookingSystem.Shared.DTOs.terminals;
using AirlineBookingSystem.Shared.Results;
using MediatR;

namespace AirlineBookingSystem.Application.Features.Terminals.Commands.Create;

/// <summary>
/// Represents a command to create a new terminal.
/// </summary>
/// <param name="Dto">The data transfer object containing the details for the new terminal.</param>
public record CreateTerminalCommand(CreateTerminalDto Dto) : IRequest<Result<int>>;

