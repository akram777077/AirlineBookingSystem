using AirlineBookingSystem.Application.Features.FlightClasses.Commands.Create;
using FluentValidation;

namespace AirlineBookingSystem.Application.Features.FlightClasses.Commands.Create;

/// <summary>
/// Validator for the <see cref="CreateFlightClassCommand"/>.
/// </summary>
public class CreateFlightClassCommandValidator : AbstractValidator<CreateFlightClassCommand>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CreateFlightClassCommandValidator"/> class.
    /// </summary>
    public CreateFlightClassCommandValidator()
    {
        RuleFor(x => x.CreateFlightClassDto.FlightId).NotEmpty();
        RuleFor(x => x.CreateFlightClassDto.ClassId).NotEmpty();
        RuleFor(x => x.CreateFlightClassDto.Price).GreaterThan(0);
        RuleFor(x => x.CreateFlightClassDto.Seats).GreaterThan(0);
    }
}
