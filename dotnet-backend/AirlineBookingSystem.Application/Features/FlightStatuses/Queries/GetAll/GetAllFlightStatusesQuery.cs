using AirlineBookingSystem.Shared.DTOs.FlightStatuses;
using AirlineBookingSystem.Shared.Results;
using MediatR;

namespace AirlineBookingSystem.Application.Features.FlightStatuses.Queries.GetAll;

public record GetAllFlightStatusesQuery : IRequest<Result<IEnumerable<FlightStatusDto>>>;
