using AirlineBookingSystem.Shared.DTOs.FlightStatuses;
using AirlineBookingSystem.Shared.Results;
using MediatR;

namespace AirlineBookingSystem.Application.Features.FlightStatuses.Queries.GetById;

/// <summary>
/// Represents a query to retrieve a flight status by its unique identifier.
/// </summary>
/// <param name="Id">The unique identifier of the flight status.</param>
public record GetFlightStatusByIdQuery(int Id) : IRequest<Result<FlightStatusDto>>;

