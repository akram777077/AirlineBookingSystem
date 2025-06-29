using FluentValidation;

namespace AirlineBookingSystem.Application.Features.Flights.Commands.Update;

public class UpdateFlightCommandValidator : AbstractValidator<UpdateFlightCommand>
{
    public UpdateFlightCommandValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("Flight Id is required.");
        RuleFor(x => x.FlightNumber)
            .NotEmpty().WithMessage("Flight number is required.")
            .MaximumLength(20);
        RuleFor(x => x.FromAirportId)
            .GreaterThan(0).WithMessage("From airport is required.");
        RuleFor(x => x.ToAirportId)
            .GreaterThan(0).WithMessage("To airport is required.");
        RuleFor(x => x.DepartureTime)
            .LessThan(x => x.ArrivalTime).WithMessage("Departure must be before arrival.");
        RuleFor(x => x.ArrivalTime)
            .GreaterThan(x => x.DepartureTime).WithMessage("Arrival must be after departure.");
    }
}
