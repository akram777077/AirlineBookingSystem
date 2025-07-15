using FluentValidation;

namespace AirlineBookingSystem.Application.Features.Terminals.Commands.UpdateTerminal;

public class UpdateTerminalCommandValidator : AbstractValidator<UpdateTerminalCommand>
{
    public UpdateTerminalCommandValidator()
    {
        RuleFor(x => x.Dto.Id)
            .GreaterThan(0).WithMessage("Terminal ID must be greater than 0.");

        RuleFor(x => x.Dto.Name)
            .NotEmpty().WithMessage("Terminal name is required.")
            .MaximumLength(100).WithMessage("Terminal name cannot exceed 100 characters.");

        RuleFor(x => x.Dto.AirportId)
            .GreaterThan(0).WithMessage("Airport ID must be greater than 0.");
    }
}
