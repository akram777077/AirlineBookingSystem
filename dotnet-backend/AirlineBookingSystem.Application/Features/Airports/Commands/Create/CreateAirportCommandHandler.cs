using AirlineBookingSystem.Application.Interfaces.UnitOfWork;
using AirlineBookingSystem.Domain.Entities;
using AirlineBookingSystem.Shared.DTOs.airports;
using AirlineBookingSystem.Shared.Results;
using AutoMapper;
using FluentValidation;
using MediatR;

namespace AirlineBookingSystem.Application.Features.Airports.Commands.Create;

public class CreateAirportCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IValidator<CreateAirportCommand> validator)
    : IRequestHandler<CreateAirportCommand, Result<AirportDto>>
{
    public async Task<Result<AirportDto>> Handle(CreateAirportCommand request, CancellationToken cancellationToken)
    {
        var validationResult = await validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            if (validationResult.Errors.Any(e => e.ErrorCode == "Conflict"))
            {
                return Result<AirportDto>.Conflict(validationResult.Errors.First().ErrorMessage);
            }
            return Result<AirportDto>.Failure(validationResult.Errors.First().ErrorMessage);
        }

        var airport = mapper.Map<Airport>(request.Airport);
        await unitOfWork.Airports.AddAsync(airport);
        await unitOfWork.CompleteAsync();
        return Result<AirportDto>.Created(mapper.Map<AirportDto>(airport));
    }
}