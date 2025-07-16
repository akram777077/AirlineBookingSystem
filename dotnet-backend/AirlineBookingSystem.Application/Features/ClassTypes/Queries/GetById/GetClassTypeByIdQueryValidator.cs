using FluentValidation;

namespace AirlineBookingSystem.Application.Features.ClassTypes.Queries.GetById;

public class GetClassTypeByIdQueryValidator : AbstractValidator<GetClassTypeByIdQuery>
{
    public GetClassTypeByIdQueryValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("Id must be greater than 0.");
    }
}
