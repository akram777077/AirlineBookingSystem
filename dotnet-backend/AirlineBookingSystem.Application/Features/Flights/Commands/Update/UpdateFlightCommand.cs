using AirlineBookingSystem.Shared.DTOs.flights;
using AirlineBookingSystem.Shared.Results;
using MediatR;

namespace AirlineBookingSystem.Application.Features.Flights.Commands.Update;

/// <summary>
/// Represents a command to update an existing flight.
/// </summary>
/// <param name="Id">The unique identifier of the flight to update.</param>
/// <param name="Dto">The data transfer object containing the updated details for the flight.</param>
public record UpdateFlightCommand(int Id, UpdateFlightDto Dto) : IRequest<Result>;
