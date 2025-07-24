using AirlineBookingSystem.Application.Interfaces.Repositories;
using AirlineBookingSystem.Application.Interfaces.UnitOfWork;
using AirlineBookingSystem.Shared.DTOs.airplanes;
using AirlineBookingSystem.Shared.Results;
using AutoMapper;
using FluentValidation;
using MediatR;

namespace AirlineBookingSystem.Application.Features.Airplanes.Commands.Update;

public class UpdateAirplaneCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IValidator<UpdateAirplaneCommand> validator)
    : IRequestHandler<UpdateAirplaneCommand, Result<AirplaneDto>>
{
    public async Task<Result<AirplaneDto>> Handle(UpdateAirplaneCommand request, CancellationToken cancellationToken)
    {
        var validationResult = await validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            if (validationResult.Errors.Any(e => e.ErrorCode == "Conflict"))
            {
                return Result<AirplaneDto>.Conflict(validationResult.Errors.First().ErrorMessage);
            }
            return Result<AirplaneDto>.Failure(validationResult.Errors.First().ErrorMessage);
        }

        var airplane = await unitOfWork.Airplanes.GetByIdAsync(request.Id);
        if (airplane == null)
        {
            return Result<AirplaneDto>.NotFound("Airplane not found."); 
        }

        mapper.Map(request.UpdateAirplaneDto, airplane);
        unitOfWork.Airplanes.Update(airplane);
        await unitOfWork.CompleteAsync();
        
        return Result<AirplaneDto>.Success(mapper.Map<AirplaneDto>(airplane));
    }
}