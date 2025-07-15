using FluentValidation;
using AirlineBookingSystem.Shared.DTOs.airplanes;

namespace AirlineBookingSystem.Application.Features.Airplanes.Commands.UpdateAirplane;

public class UpdateAirplaneCommandValidator : AbstractValidator<UpdateAirplaneCommand>
{
    public UpdateAirplaneCommandValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0).WithMessage("Id must be greater than 0.");
        RuleFor(x => x.UpdateAirplaneDto.Model).NotEmpty().WithMessage("Model is required.");
        RuleFor(x => x.UpdateAirplaneDto.Manufacturer).NotEmpty().WithMessage("Manufacturer is required.");
        RuleFor(x => x.UpdateAirplaneDto.Capacity).GreaterThan(0).WithMessage("Capacity must be greater than 0.");
        RuleFor(x => x.UpdateAirplaneDto.Code).NotEmpty().WithMessage("Code is required.");
    }
}