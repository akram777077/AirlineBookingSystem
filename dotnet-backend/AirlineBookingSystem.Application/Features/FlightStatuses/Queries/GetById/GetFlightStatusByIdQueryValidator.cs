using FluentValidation;

namespace AirlineBookingSystem.Application.Features.FlightStatuses.Queries.GetById;

public class GetFlightStatusByIdQueryValidator : AbstractValidator<GetFlightStatusByIdQuery>
{
    public GetFlightStatusByIdQueryValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("Id must be greater than 0.");
    }
}
