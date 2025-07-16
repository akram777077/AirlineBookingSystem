using FluentValidation;

namespace AirlineBookingSystem.Application.Features.Airplanes.Queries.GetById;

public class GetAirplaneByIdQueryValidator : AbstractValidator<GetAirplaneByIdQuery>
{
    public GetAirplaneByIdQueryValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0).WithMessage("Id must be greater than 0.");
    }
}