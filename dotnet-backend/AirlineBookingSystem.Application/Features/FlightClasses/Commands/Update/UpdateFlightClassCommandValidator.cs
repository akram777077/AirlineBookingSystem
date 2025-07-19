using AirlineBookingSystem.Application.Features.FlightClasses.Commands.Update;
using FluentValidation;

namespace AirlineBookingSystem.Application.Features.FlightClasses.Commands.Update;

public class UpdateFlightClassCommandValidator : AbstractValidator<UpdateFlightClassCommand>
{
    public UpdateFlightClassCommandValidator()
    {
        RuleFor(x => x.UpdateFlightClassDto.Id).NotEmpty();
        RuleFor(x => x.UpdateFlightClassDto.Price).GreaterThan(0);
        RuleFor(x => x.UpdateFlightClassDto.Seats).GreaterThan(0);
    }
}