using AirlineBookingSystem.Shared.DTOs.Gates;
using AirlineBookingSystem.Shared.Results;
using MediatR;

namespace AirlineBookingSystem.Application.Features.Gates.Commands.Create;

/// <summary>
/// Represents a command to create a new gate.
/// </summary>
/// <param name="Dto">The data transfer object containing the details for the new gate.</param>
public record CreateGateCommand(CreateGateDto Dto) : IRequest<Result<int>>;

