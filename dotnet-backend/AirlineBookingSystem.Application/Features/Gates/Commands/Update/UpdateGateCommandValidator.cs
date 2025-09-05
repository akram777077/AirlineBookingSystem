using FluentValidation;

namespace AirlineBookingSystem.Application.Features.Gates.Commands.Update;

/// <summary>
/// Validator for the <see cref="UpdateGateCommand"/>.
/// </summary>
public class UpdateGateCommandValidator : AbstractValidator<UpdateGateCommand>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="UpdateGateCommandValidator"/> class.
    /// </summary>
    public UpdateGateCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Id is required.");

        RuleFor(x => x.Dto.GateNumber)
            .NotEmpty().WithMessage("Gate number is required.")
            .MaximumLength(10).WithMessage("Gate number cannot exceed 10 characters.");

        RuleFor(x => x.Dto.TerminalId)
            .NotEmpty().WithMessage("Terminal ID is required.");
    }
}
