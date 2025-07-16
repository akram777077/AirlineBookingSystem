using FluentValidation;

namespace AirlineBookingSystem.Application.Features.Genders.Queries.GetById;

public class GetGenderByIdQueryValidator : AbstractValidator<GetGenderByIdQuery>
{
    public GetGenderByIdQueryValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("Id must be greater than 0.");
    }
}
