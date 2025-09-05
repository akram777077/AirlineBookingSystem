using AirlineBookingSystem.Shared.DTOs.airports;
using AirlineBookingSystem.Shared.Results;
using MediatR;

namespace AirlineBookingSystem.Application.Features.Airports.Queries.GetById;

/// <summary>
/// Represents a query to retrieve an airport by its unique identifier.
/// </summary>
/// <param name="Id">The unique identifier of the airport.</param>
public record GetAirportByIdQuery(int Id) : IRequest<Result<AirportDto>>;