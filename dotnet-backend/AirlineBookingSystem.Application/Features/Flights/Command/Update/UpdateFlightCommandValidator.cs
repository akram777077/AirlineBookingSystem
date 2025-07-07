using AirlineBookingSystem.Application.Interfaces.UnitOfWork;
using FluentValidation;

namespace AirlineBookingSystem.Application.Features.Flights.Command.Update;

public class UpdateFlightCommandValidator : AbstractValidator<UpdateFlightCommand>
{
    public UpdateFlightCommandValidator(IUnitOfWork unitOfWork)
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Flight ID is required.");

        RuleFor(x => x.Dto)
            .NotNull().WithMessage("Update flight DTO cannot be null.");

        RuleFor(x => x.Dto.DepartureTime)
            .NotEmpty().WithMessage("Departure time is required.")
            .MustAsync(async (command, departureTime, cancellation) =>
            {
                var existingFlight = await unitOfWork.Flights.GetByIdAsync(command.Id);
                if (existingFlight == null)
                {
                    return true; // Handled by the handler
                }
                return departureTime >= existingFlight.DepartureTime;
            }).WithMessage("Departure time cannot be earlier than the original departure time.");

        RuleFor(x => x.Dto.AirplaneId)
            .NotEmpty().WithMessage("Airplane ID is required.")
            .GreaterThan(0).WithMessage("Airplane ID must be greater than zero.");

        RuleFor(x => x.Dto.DepartureGateId)
            .NotEmpty().WithMessage("Departure gate ID is required.")
            .GreaterThan(0).WithMessage("Departure gate ID must be greater than zero.");
    }
}