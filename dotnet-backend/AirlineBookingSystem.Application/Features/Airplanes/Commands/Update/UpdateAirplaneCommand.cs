using AirlineBookingSystem.Domain.Entities;
using AirlineBookingSystem.Shared.DTOs.airplanes;
using MediatR;
using AirlineBookingSystem.Shared.Results;

namespace AirlineBookingSystem.Application.Features.Airplanes.Commands.UpdateAirplane;

public class UpdateAirplaneCommand(int id, UpdateAirplaneDto updateAirplaneDto) : IRequest<Result<AirplaneDto>>
{
    public int Id { get; } = id;
    public UpdateAirplaneDto UpdateAirplaneDto { get; } = updateAirplaneDto;
}