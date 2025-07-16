using FluentValidation;

namespace AirlineBookingSystem.Application.Features.Gates.Queries.GetById;

public class GetGateByIdQueryValidator : AbstractValidator<GetGateByIdQuery>
{
    public GetGateByIdQueryValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Id is required.");
    }
}
