using AirlineBookingSystem.Shared.DTOs.airplanes;
using MediatR;
using AirlineBookingSystem.Shared.Results;

namespace AirlineBookingSystem.Application.Features.Airplanes.Queries.GetById;

public record GetAirplaneByIdQuery (int Id) : IRequest<Result<AirplaneDto>>;