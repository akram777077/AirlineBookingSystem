using FluentValidation;

namespace AirlineBookingSystem.Application.Features.Flights.Commands.Create;

public class CreateFlightCommandValidator : AbstractValidator<CreateFlightCommand>
{
    public CreateFlightCommandValidator()
    {
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
        RuleFor(x => x.Price)
            .GreaterThanOrEqualTo(0).WithMessage("Price must be non-negative.");
    }
}
