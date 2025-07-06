using FluentValidation;

namespace AirlineBookingSystem.Application.Features.Flights.Command.Delete;

public class DeleteFlightCommandValidator : AbstractValidator<DeleteFlightCommand>
{
    public DeleteFlightCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Flight ID is required.")
            .GreaterThan(0).WithMessage("Flight ID must be greater than zero.");
    }
}