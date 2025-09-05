using FluentValidation;

namespace AirlineBookingSystem.Application.Features.Airports.Queries.GetById;

/// <summary>
/// Validator for the <see cref="GetAirportByIdQuery"/>.
/// </summary>
public class GetAirportByIdQueryValidator : AbstractValidator<GetAirportByIdQuery>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="GetAirportByIdQueryValidator"/> class.
    /// </summary>
    public GetAirportByIdQueryValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("Id must be greater than 0.");
    }
}