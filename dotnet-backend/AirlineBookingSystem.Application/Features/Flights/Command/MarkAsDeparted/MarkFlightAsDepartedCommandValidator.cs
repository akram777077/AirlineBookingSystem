using AirlineBookingSystem.Application.Interfaces.UnitOfWork;
using FluentValidation;

namespace AirlineBookingSystem.Application.Features.Flights.Command.MarkAsDeparted;

public class MarkFlightAsDepartedCommandValidator : AbstractValidator<MarkFlightAsDepartedCommand>
{
    public MarkFlightAsDepartedCommandValidator(IUnitOfWork unitOfWork)
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Flight ID is required.")
            .GreaterThan(0).WithMessage("Flight ID must be greater than zero.")
            .MustAsync(async (id, cancellation) =>
            {
                var flight = await unitOfWork.Flights.GetByIdAsync(id);
                if (flight == null)
                {
                    return true; // Flight not found, handled by handler
                }
                return DateTimeOffset.UtcNow >= flight.DepartureTime;
            }).WithMessage("Flight cannot be marked as departed before its scheduled departure time.");
    }
}