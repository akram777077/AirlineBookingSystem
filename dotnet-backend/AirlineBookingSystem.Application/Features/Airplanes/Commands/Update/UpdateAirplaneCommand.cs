using AirlineBookingSystem.Shared.DTOs.airplanes;
using AirlineBookingSystem.Shared.Results;
using MediatR;

namespace AirlineBookingSystem.Application.Features.Airplanes.Commands.Update;

public record UpdateAirplaneCommand(int Id, UpdateAirplaneDto UpdateAirplaneDto) : IRequest<Result<AirplaneDto>>;