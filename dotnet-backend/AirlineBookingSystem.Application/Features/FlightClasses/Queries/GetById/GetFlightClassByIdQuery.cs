using AirlineBookingSystem.Shared.DTOs.flightClasses;
using AirlineBookingSystem.Shared.Results;
using MediatR;

namespace AirlineBookingSystem.Application.Features.FlightClasses.Queries.GetById;

/// <summary>
/// Represents a query to retrieve a flight class by its unique identifier.
/// </summary>
/// <param name="Id">The unique identifier of the flight class.</param>
public record GetFlightClassByIdQuery(int Id) : IRequest<Result<FlightClassDto>>;

