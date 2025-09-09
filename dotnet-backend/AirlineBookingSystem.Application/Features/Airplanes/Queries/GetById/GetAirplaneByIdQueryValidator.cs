using FluentValidation;

namespace AirlineBookingSystem.Application.Features.Airplanes.Queries.GetById;

/// <summary>
/// Validator for the <see cref="GetAirplaneByIdQuery"/>.
/// </summary>
public class GetAirplaneByIdQueryValidator : AbstractValidator<GetAirplaneByIdQuery>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="GetAirplaneByIdQueryValidator"/> class.
    /// </summary>
    public GetAirplaneByIdQueryValidator()
    {
        RuleFor(p => p.Id)
            .GreaterThan(0).WithMessage("Id must be greater than 0.");
    }
}