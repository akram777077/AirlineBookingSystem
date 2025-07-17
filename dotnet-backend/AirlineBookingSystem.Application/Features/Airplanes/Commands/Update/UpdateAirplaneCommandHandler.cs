using AirlineBookingSystem.Application.Interfaces.Repositories;
using AirlineBookingSystem.Shared.DTOs.airplanes;
using AirlineBookingSystem.Shared.Results;
using AutoMapper;
using MediatR;

namespace AirlineBookingSystem.Application.Features.Airplanes.Commands.Update;

public class UpdateAirplaneCommandHandler(IAirplaneRepository airplaneRepository, IMapper mapper)
    : IRequestHandler<UpdateAirplaneCommand, Result<AirplaneDto>>
{
    public async Task<Result<AirplaneDto>> Handle(UpdateAirplaneCommand request, CancellationToken cancellationToken)
    {
        var airplane = await airplaneRepository.GetByIdAsync(request.Id);
        if (airplane == null)
        {
            return Result<AirplaneDto>.NotFound("Airplane not found."); 
        }

        mapper.Map(request.UpdateAirplaneDto, airplane);
        airplaneRepository.Update(airplane);
        return Result<AirplaneDto>.Success(mapper.Map<AirplaneDto>(airplane));
    }
}