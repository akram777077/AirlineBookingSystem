using FluentValidation;

namespace AirlineBookingSystem.Application.Features.Cities.Queries.GetById;

public class GetCityByIdQueryValidator : AbstractValidator<GetCityByIdQuery>
{
    public GetCityByIdQueryValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("City ID must be greater than 0.");
    }
}