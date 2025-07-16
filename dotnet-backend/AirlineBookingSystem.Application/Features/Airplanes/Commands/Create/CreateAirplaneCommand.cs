using AirlineBookingSystem.Shared.DTOs.airplanes;
using MediatR;
using AirlineBookingSystem.Shared.Results;

namespace AirlineBookingSystem.Application.Features.Airplanes.Commands.CreateAirplane;

public record CreateAirplaneCommand(CreateAirplaneDto CreateAirplaneDto) : IRequest<Result<AirplaneDto>>;