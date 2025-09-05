using AirlineBookingSystem.Shared.DTOs.terminals;
using AirlineBookingSystem.Shared.Results;
using MediatR;

namespace AirlineBookingSystem.Application.Features.Terminals.Commands.Update;

/// <summary>
/// Represents a command to update an existing terminal.
/// </summary>
/// <param name="Dto">The data transfer object containing the updated details for the terminal.</param>
public record UpdateTerminalCommand(UpdateTerminalDto Dto) : IRequest<Result>;
