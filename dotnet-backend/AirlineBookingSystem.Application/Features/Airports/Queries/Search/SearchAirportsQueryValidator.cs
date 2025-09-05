using FluentValidation;

namespace AirlineBookingSystem.Application.Features.Airports.Queries.Search;

/// <summary>
/// Validator for the <see cref="SearchAirportsQuery"/>.
/// </summary>
public class SearchAirportsQueryValidator : AbstractValidator<SearchAirportsQuery>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="SearchAirportsQueryValidator"/> class.
    /// </summary>
    public SearchAirportsQueryValidator()
    {
        RuleFor(x => x.Filter.PageNumber)
            .GreaterThanOrEqualTo(1).WithMessage("PageNumber at least greater than or equal to 1.");

        RuleFor(x => x.Filter.PageSize)
            .GreaterThanOrEqualTo(1).WithMessage("PageSize at least greater than or equal to 1.");
    }
}