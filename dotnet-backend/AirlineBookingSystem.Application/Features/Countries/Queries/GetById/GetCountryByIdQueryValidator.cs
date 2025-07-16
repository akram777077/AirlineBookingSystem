using FluentValidation;

namespace AirlineBookingSystem.Application.Features.Countries.Queries.GetById;

public class GetCountryByIdQueryValidator : AbstractValidator<GetCountryByIdQuery>
{
    public GetCountryByIdQueryValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("Id must be greater than 0.");
    }
}
