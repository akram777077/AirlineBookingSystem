using AirlineBookingSystem.Shared.Filters;
using FluentValidation;

namespace AirlineBookingSystem.Application.Features.Airplanes.Queries.Search;

/// <summary>
/// Validator for the <see cref="SearchAirplanesQuery"/>.
/// </summary>
public class SearchAirplanesQueryValidator : AbstractValidator<SearchAirplanesQuery>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="SearchAirplanesQueryValidator"/> class.
    /// </summary>
    public SearchAirplanesQueryValidator()
    {
        RuleFor(x => x.PageNumber)
            .GreaterThanOrEqualTo(1).WithMessage("PageNumber at least greater than or equal to 1.");

        RuleFor(x => x.PageSize)
            .GreaterThanOrEqualTo(1).WithMessage("PageSize at least greater than or equal to 1.");
    }
}
