using FluentValidation;
using AirlineBookingSystem.Shared.DTOs.airplanes;

namespace AirlineBookingSystem.Application.Features.Airplanes.Commands.CreateAirplane;

public class CreateAirplaneCommandValidator : AbstractValidator<CreateAirplaneCommand>
{
    public CreateAirplaneCommandValidator()
    {
        RuleFor(x => x.CreateAirplaneDto.Model).NotEmpty().WithMessage("Model is required.");
        RuleFor(x => x.CreateAirplaneDto.Manufacturer).NotEmpty().WithMessage("Manufacturer is required.");
        RuleFor(x => x.CreateAirplaneDto.Capacity).GreaterThan(0).WithMessage("Capacity must be greater than 0.");
        RuleFor(x => x.CreateAirplaneDto.Code).NotEmpty().WithMessage("Code is required.");
    }
}