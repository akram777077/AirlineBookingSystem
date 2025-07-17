using FluentValidation;

namespace AirlineBookingSystem.Application.Features.Gates.Commands.Create;

public class CreateGateCommandValidator : AbstractValidator<CreateGateCommand>
{
    public CreateGateCommandValidator()
    {
        RuleFor(x => x.Dto.GateNumber)
            .NotEmpty().WithMessage("Gate number is required.")
            .MaximumLength(10).WithMessage("Gate number cannot exceed 10 characters.");

        RuleFor(x => x.Dto.TerminalId)
            .NotEmpty().WithMessage("Terminal ID is required.");
    }
}
