using FluentValidation;

namespace AirlineBookingSystem.Application.Features.Airports.Command.Update;

public class UpdateAirportCommandValidator : AbstractValidator<UpdateAirportCommand>
{
    public UpdateAirportCommandValidator()
    {
        RuleFor(x => x.Airport.Id)
            .GreaterThan(0).WithMessage("Id must be greater than 0.");

        RuleFor(x => x.Airport.AirportCode)
            .NotEmpty().WithMessage("AirportCode is required.")
            .Length(3).WithMessage("AirportCode must be 3 characters long.");

        RuleFor(x => x.Airport.Name)
            .NotEmpty().WithMessage("Name is required.");

        RuleFor(x => x.Airport.CityId)
            .GreaterThan(0).WithMessage("CityId must be greater than 0.");

        RuleFor(x => x.Airport.Timezone)
            .NotEmpty().WithMessage("Timezone is required.");
    }
}