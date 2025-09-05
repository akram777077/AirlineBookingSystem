using FluentValidation;

namespace AirlineBookingSystem.Application.Features.Flights.Commands.Create
{
    /// <summary>
    /// Validator for the <see cref="CreateFlightCommand"/>.
    /// </summary>
    public class CreateFlightCommandValidator : AbstractValidator<CreateFlightCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CreateFlightCommandValidator"/> class.
        /// </summary>
        public CreateFlightCommandValidator()
        {

            RuleFor(x => x.Dto.DepartureTime)
                .NotEmpty().WithMessage("Departure time is required.")
                .GreaterThan(DateTimeOffset.UtcNow).WithMessage("Departure time must be in the future.");

            RuleFor(x => x.Dto.ArrivalTime)
                .GreaterThan(x => x.Dto.DepartureTime).WithMessage("Arrival time must be after departure time.")
                .When(x => x.Dto.ArrivalTime.HasValue);

            RuleFor(x => x.Dto.AirplaneId)
                .GreaterThan(0).WithMessage("Airplane ID must be greater than 0.");

            RuleFor(x => x.Dto.DepartureGateId)
                .GreaterThan(0).WithMessage("Departure gate ID must be greater than 0.");
        }
    }
}
