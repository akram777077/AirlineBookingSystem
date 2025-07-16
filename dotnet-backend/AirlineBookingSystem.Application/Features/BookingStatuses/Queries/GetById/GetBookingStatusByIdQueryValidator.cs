using FluentValidation;

namespace AirlineBookingSystem.Application.Features.BookingStatuses.Queries.GetById;

public class GetBookingStatusByIdQueryValidator : AbstractValidator<GetBookingStatusByIdQuery>
{
    public GetBookingStatusByIdQueryValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("Id must be greater than 0.");
    }
}
