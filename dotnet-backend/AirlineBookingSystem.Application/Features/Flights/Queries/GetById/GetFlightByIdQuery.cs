using AirlineBookingSystem.Shared.DTOs.flights;
using AirlineBookingSystem.Shared.Results;
using MediatR;

namespace AirlineBookingSystem.Application.Features.Flights.Queries.GetById;

/// <summary>
/// Represents a query to retrieve flight details by its unique identifier.
/// </summary>
/// <param name="Id">The unique identifier of the flight.</param>
public record GetFlightByIdQuery (int Id) : IRequest<Result<FlightDetailsDto>>;