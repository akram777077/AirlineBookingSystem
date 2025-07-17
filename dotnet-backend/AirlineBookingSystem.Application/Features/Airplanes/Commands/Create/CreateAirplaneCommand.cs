using AirlineBookingSystem.Shared.DTOs.airplanes;
using AirlineBookingSystem.Shared.Results;
using MediatR;

namespace AirlineBookingSystem.Application.Features.Airplanes.Commands.Create;

public record CreateAirplaneCommand(CreateAirplaneDto CreateAirplaneDto) : IRequest<Result<AirplaneDto>>;