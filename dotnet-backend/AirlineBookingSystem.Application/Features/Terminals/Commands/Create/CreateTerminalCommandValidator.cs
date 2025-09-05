using FluentValidation;

namespace AirlineBookingSystem.Application.Features.Terminals.Commands.Create;

/// <summary>
/// Validator for the <see cref="CreateTerminalCommand"/>.
/// </summary>
public class CreateTerminalCommandValidator : AbstractValidator<CreateTerminalCommand>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CreateTerminalCommandValidator"/> class.
    /// </summary>
    public CreateTerminalCommandValidator()
    {
        RuleFor(x => x.Dto.Name)
            .NotEmpty().WithMessage("Terminal name is required.")
            .MaximumLength(100).WithMessage("Terminal name cannot exceed 100 characters.");

        RuleFor(x => x.Dto.AirportId)
            .GreaterThan(0).WithMessage("Airport ID must be greater than 0.");
    }
}
