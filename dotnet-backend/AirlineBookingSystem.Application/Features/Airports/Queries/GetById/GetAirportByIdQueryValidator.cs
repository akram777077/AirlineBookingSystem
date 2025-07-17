using FluentValidation;

namespace AirlineBookingSystem.Application.Features.Airports.Queries.GetById;

public class GetAirportByIdQueryValidator : AbstractValidator<GetAirportByIdQuery>
{
    public GetAirportByIdQueryValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("Id must be greater than 0.");
    }
}