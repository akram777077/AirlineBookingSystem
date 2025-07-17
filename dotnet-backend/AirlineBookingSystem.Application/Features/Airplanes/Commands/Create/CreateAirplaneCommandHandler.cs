using AirlineBookingSystem.Application.Interfaces.UnitOfWork;
using AirlineBookingSystem.Domain.Entities;
using AirlineBookingSystem.Shared.DTOs.airplanes;
using AirlineBookingSystem.Shared.Results;
using AutoMapper;
using MediatR;

namespace AirlineBookingSystem.Application.Features.Airplanes.Commands.Create;

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