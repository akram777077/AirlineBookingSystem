using FluentValidation;

namespace AirlineBookingSystem.Application.Features.Gates.Commands.UpdateGate;

public class UpdateGateCommandValidator : AbstractValidator<UpdateGateCommand>
{
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
