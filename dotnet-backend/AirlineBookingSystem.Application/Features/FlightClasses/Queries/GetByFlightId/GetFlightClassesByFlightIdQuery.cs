using AirlineBookingSystem.Shared.DTOs.flightClasses;
using AirlineBookingSystem.Shared.Results;
using MediatR;

namespace AirlineBookingSystem.Application.Features.FlightClasses.Queries.GetByFlightId;

/// <summary>
/// Represents a query to retrieve all flight classes associated with a specific flight ID.
/// </summary>
/// <param name="FlightId">The unique identifier of the flight.</param>
public record GetFlightClassesByFlightIdQuery(int FlightId) : IRequest<Result<IEnumerable<FlightClassDto>>>;

