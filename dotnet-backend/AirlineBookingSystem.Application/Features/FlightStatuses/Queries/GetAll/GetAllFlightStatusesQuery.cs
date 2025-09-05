using AirlineBookingSystem.Shared.DTOs.FlightStatuses;
using AirlineBookingSystem.Shared.Results;
using MediatR;

namespace AirlineBookingSystem.Application.Features.FlightStatuses.Queries.GetAll;

/// <summary>
/// Represents a query to retrieve all flight statuses.
/// </summary>
public record GetAllFlightStatusesQuery : IRequest<Result<IEnumerable<FlightStatusDto>>>;

