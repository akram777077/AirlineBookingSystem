using AirlineBookingSystem.Shared.DTOs.flights;
using AirlineBookingSystem.Shared.Results;
using MediatR;

namespace AirlineBookingSystem.Application.Features.Flights.Commands.Create;

/// <summary>
/// Represents a command to create a new flight.
/// </summary>
/// <param name="Dto">The data transfer object containing the details for the new flight.</param>
public record CreateFlightCommand(CreateFlightDto Dto) : IRequest<Result<int>>;

