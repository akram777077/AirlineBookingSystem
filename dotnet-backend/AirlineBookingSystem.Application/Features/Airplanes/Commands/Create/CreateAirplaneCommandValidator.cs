using FluentValidation;

namespace AirlineBookingSystem.Application.Features.Airplanes.Commands.Create;

/// <summary>
/// Validator for the <see cref="CreateAirplaneCommand"/>.
/// </summary>
public class CreateAirplaneCommandValidator : AbstractValidator<CreateAirplaneCommand>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CreateAirplaneCommandValidator"/> class.
    /// </summary>
    public CreateAirplaneCommandValidator()
    {
        RuleFor(p => p.Model)
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .NotNull()
            .MaximumLength(50).WithMessage("{PropertyName} must not exceed 50 characters.");

        RuleFor(p => p.Manufacturer)
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .NotNull()
            .MaximumLength(50).WithMessage("{PropertyName} must not exceed 50 characters.");

        RuleFor(p => p.Capacity)
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .GreaterThan(0).WithMessage("{PropertyName} must be greater than 0.");
        
        RuleFor(p => p.Code)
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .NotNull()
            .MaximumLength(10).WithMessage("{PropertyName} must not exceed 10 characters.");
    }
}