using AirlineBookingSystem.Domain.Entities;
using AirlineBookingSystem.Shared.DTOs.airplanes;
using MediatR;
using AirlineBookingSystem.Shared.Results;

namespace AirlineBookingSystem.Application.Features.Airplanes.Commands.UpdateAirplane;

public record UpdateAirplaneCommand(int Id, UpdateAirplaneDto UpdateAirplaneDto) : IRequest<Result<AirplaneDto>>;