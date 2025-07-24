using AirlineBookingSystem.Application.Interfaces.UnitOfWork;
using FluentValidation;

namespace AirlineBookingSystem.Application.Features.Airports.Commands.Create;

public class CreateAirportCommandValidator : AbstractValidator<CreateAirportCommand>
{
    private readonly IUnitOfWork _unitOfWork;

    public CreateAirportCommandValidator(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;

        RuleFor(x => x.Airport.AirportCode)
            .NotEmpty().WithMessage("AirportCode is required.")
            .Length(3).WithMessage("AirportCode must be 3 characters long.")
            .MustAsync(async (code, cancellation) =>
            {
                return await _unitOfWork.Airports.GetByCodeAsync(code) == null;
            }).WithMessage("An airport with this code already exists.").WithErrorCode("Conflict");

        RuleFor(x => x.Airport.Name)
            .NotEmpty().WithMessage("Name is required.");

        RuleFor(x => x.Airport.CityId)
            .GreaterThan(0).WithMessage("CityId must be greater than 0.");

        RuleFor(x => x.Airport.Timezone)
            .NotEmpty().WithMessage("Timezone is required.");
    }
}