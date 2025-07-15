using AirlineBookingSystem.Application.Interfaces.UnitOfWork;
using AirlineBookingSystem.Domain.Entities;
using AirlineBookingSystem.Shared.DTOs.airplanes;
using AutoMapper;
using MediatR;
using AirlineBookingSystem.Shared.Results;

namespace AirlineBookingSystem.Application.Features.Airplanes.Commands.CreateAirplane;

public class CreateAirplaneCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    : IRequestHandler<CreateAirplaneCommand, Result<AirplaneDto>>
{
    public async Task<Result<AirplaneDto>> Handle(CreateAirplaneCommand request, CancellationToken cancellationToken)
    {
        var airplane = mapper.Map<Airplane>(request.CreateAirplaneDto);
        await unitOfWork.Airplanes.AddAsync(airplane);
        return Result<AirplaneDto>.Success(mapper.Map<AirplaneDto>(airplane));
    }
}