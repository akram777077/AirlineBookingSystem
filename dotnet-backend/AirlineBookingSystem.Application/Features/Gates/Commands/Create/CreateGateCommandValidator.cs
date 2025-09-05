using FluentValidation;

namespace AirlineBookingSystem.Application.Features.Gates.Commands.Create;

/// <summary>
/// Validator for the <see cref="CreateGateCommand"/>.
/// </summary>
public class CreateGateCommandValidator : AbstractValidator<CreateGateCommand>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CreateGateCommandValidator"/> class.
    /// </summary>
    public CreateGateCommandValidator()
    {
        RuleFor(x => x.Dto.GateNumber)
            .NotEmpty().WithMessage("Gate number is required.")
            .MaximumLength(10).WithMessage("Gate number cannot exceed 10 characters.");

        RuleFor(x => x.Dto.TerminalId)
            .NotEmpty().WithMessage("Terminal ID is required.");
    }
}
