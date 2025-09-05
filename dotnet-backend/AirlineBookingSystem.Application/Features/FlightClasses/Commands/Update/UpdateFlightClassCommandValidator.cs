using AirlineBookingSystem.Application.Features.FlightClasses.Commands.Update;
using FluentValidation;

namespace AirlineBookingSystem.Application.Features.FlightClasses.Commands.Update;

/// <summary>
/// Validator for the <see cref="UpdateFlightClassCommand"/>.
/// </summary>
public class UpdateFlightClassCommandValidator : AbstractValidator<UpdateFlightClassCommand>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="UpdateFlightClassCommandValidator"/> class.
    /// </summary>
    public UpdateFlightClassCommandValidator()
    {
        RuleFor(x => x.UpdateFlightClassDto.Id).NotEmpty();
        RuleFor(x => x.UpdateFlightClassDto.Price).GreaterThan(0);
        RuleFor(x => x.UpdateFlightClassDto.Seats).GreaterThan(0);
    }
}