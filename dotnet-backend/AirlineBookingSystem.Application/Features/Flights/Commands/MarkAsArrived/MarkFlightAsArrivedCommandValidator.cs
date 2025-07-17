using AirlineBookingSystem.Application.Interfaces.UnitOfWork;
using AirlineBookingSystem.Shared.Enums;
using FluentValidation;

namespace AirlineBookingSystem.Application.Features.Flights.Commands.MarkAsArrived;

public class MarkFlightAsArrivedCommandValidator : AbstractValidator<MarkFlightAsArrivedCommand>
{
    public MarkFlightAsArrivedCommandValidator(IUnitOfWork unitOfWork)
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
                return flight.FlightStatus.StatusName == FlightStatusEnum.Departed;
            }).WithMessage("Flight must be departed to be marked as arrived.");
    }
}