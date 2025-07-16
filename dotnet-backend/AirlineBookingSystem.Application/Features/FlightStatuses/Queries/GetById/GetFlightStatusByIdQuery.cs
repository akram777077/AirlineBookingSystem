using AirlineBookingSystem.Shared.DTOs.FlightStatuses;
using AirlineBookingSystem.Shared.Results;
using MediatR;

namespace AirlineBookingSystem.Application.Features.FlightStatuses.Queries.GetById;

public record GetFlightStatusByIdQuery(int Id) : IRequest<Result<FlightStatusDto>>;
