using FluentValidation;

namespace AirlineBookingSystem.Application.CQRS.Countries.Commands.Create;

public class CreateCountryCommandValidator : AbstractValidator<CreateCountryCommand>
{
    public CreateCountryCommandValidator()
    {
        RuleFor(c => c.Name)
            .NotEmpty().WithMessage("Country name is required.")
            .MaximumLength(100).WithMessage("Country name must not exceed 100 characters.");

        RuleFor(c => c.Code)
            .NotEmpty().WithMessage("Country code is required.")
            .Length(2, 3).WithMessage("Country code must be between 2 and 3 characters long.")
            .Matches("^[A-Z]{2,3}$").WithMessage("Country code must consist of uppercase letters only.");
    }
}