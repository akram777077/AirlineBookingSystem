using AirlineBookingSystem.Shared.DTOs.airplanes;
using MediatR;
using AirlineBookingSystem.Shared.Results;

namespace AirlineBookingSystem.Application.Features.Airplanes.Commands.CreateAirplane;

public class CreateAirplaneCommand : IRequest<Result<AirplaneDto>>
{
    public CreateAirplaneDto CreateAirplaneDto { get; }

    public CreateAirplaneCommand(CreateAirplaneDto createAirplaneDto)
    {
        CreateAirplaneDto = createAirplaneDto;
    }
}