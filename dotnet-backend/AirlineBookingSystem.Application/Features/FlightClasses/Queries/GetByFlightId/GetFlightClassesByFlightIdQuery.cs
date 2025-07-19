using AirlineBookingSystem.Shared.DTOs.flightClasses;
using AirlineBookingSystem.Shared.Results;
using MediatR;

namespace AirlineBookingSystem.Application.Features.FlightClasses.Queries.GetByFlightId;

public record GetFlightClassesByFlightIdQuery(int FlightId) : IRequest<Result<IEnumerable<FlightClassDto>>>;
