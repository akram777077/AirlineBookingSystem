using AirlineBookingSystem.Shared.DTOs.airports;
using AirlineBookingSystem.Shared.Results;
using MediatR;

namespace AirlineBookingSystem.Application.Features.Airports.Commands.Update;

/// <summary>
/// Represents a command to update an existing airport.
/// </summary>
/// <param name="Airport">The data transfer object containing the updated details for the airport.</param>
public record UpdateAirportCommand(UpdateAirportDto Airport) : IRequest<Result<AirportDto>>;