using AirlineBookingSystem.Application.Interfaces.UnitOfWork;
using AirlineBookingSystem.Domain.Entities;
using AirlineBookingSystem.Shared.DTOs.airplanes;
using AirlineBookingSystem.Shared.Results;
using AutoMapper;
using FluentValidation;
using MediatR;

namespace AirlineBookingSystem.Application.Features.Airplanes.Commands.Create;

public class CreateAirplaneCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IValidator<CreateAirplaneCommand> validator)
    : IRequestHandler<CreateAirplaneCommand, Result<AirplaneDto>>
{
    public async Task<Result<AirplaneDto>> Handle(CreateAirplaneCommand request, CancellationToken cancellationToken)
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

        var airplane = mapper.Map<Airplane>(request.CreateAirplaneDto);
        await unitOfWork.Airplanes.AddAsync(airplane);
        await unitOfWork.CompleteAsync();
        return Result<AirplaneDto>.Created(mapper.Map<AirplaneDto>(airplane));
    }
}