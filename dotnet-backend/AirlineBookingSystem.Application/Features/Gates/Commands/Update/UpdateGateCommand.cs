using AirlineBookingSystem.Shared.DTOs.Gates;
using AirlineBookingSystem.Shared.Results;
using MediatR;

namespace AirlineBookingSystem.Application.Features.Gates.Commands.Update;

/// <summary>
/// Represents a command to update an existing gate.
/// </summary>
/// <param name="Id">The unique identifier of the gate to update.</param>
/// <param name="Dto">The data transfer object containing the updated details for the gate.</param>
public record UpdateGateCommand(int Id, UpdateGateDto Dto) : IRequest<Result>;
