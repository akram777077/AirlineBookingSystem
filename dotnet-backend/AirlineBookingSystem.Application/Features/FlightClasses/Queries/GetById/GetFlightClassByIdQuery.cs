using AirlineBookingSystem.Shared.DTOs.flightClasses;
using AirlineBookingSystem.Shared.Results;
using MediatR;

namespace AirlineBookingSystem.Application.Features.FlightClasses.Queries.GetById;

public record GetFlightClassByIdQuery(int Id) : IRequest<Result<FlightClassDto>>;
