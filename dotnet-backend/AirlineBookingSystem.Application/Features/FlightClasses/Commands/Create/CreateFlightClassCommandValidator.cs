using AirlineBookingSystem.Application.Features.FlightClasses.Commands.Create;
using FluentValidation;

namespace AirlineBookingSystem.Application.Features.FlightClasses.Commands.Create;

public class CreateFlightClassCommandValidator : AbstractValidator<CreateFlightClassCommand>
{
    public CreateFlightClassCommandValidator()
    {
        RuleFor(x => x.CreateFlightClassDto.FlightId).NotEmpty();
        RuleFor(x => x.CreateFlightClassDto.ClassId).NotEmpty();
        RuleFor(x => x.CreateFlightClassDto.Price).GreaterThan(0);
        RuleFor(x => x.CreateFlightClassDto.Seats).GreaterThan(0);
    }
}
