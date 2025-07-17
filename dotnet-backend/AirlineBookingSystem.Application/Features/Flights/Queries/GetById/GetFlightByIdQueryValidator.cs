using FluentValidation;

namespace AirlineBookingSystem.Application.Features.Flights.Queries.GetById;

public class GetFlightByIdQueryValidator : AbstractValidator<GetFlightByIdQuery>
{
    public GetFlightByIdQueryValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Flight ID is required.")
            .GreaterThan(0).WithMessage("Flight ID must be greater than zero.");
    }
}