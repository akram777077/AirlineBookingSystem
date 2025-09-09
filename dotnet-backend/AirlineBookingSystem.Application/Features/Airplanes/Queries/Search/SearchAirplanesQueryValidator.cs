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
            .GreaterThanOrEqualTo(1).WithMessage("Page number must be at least 1.");

        RuleFor(x => x.PageSize)
            .GreaterThanOrEqualTo(1).WithMessage("Page size must be at least 1.")
            .LessThanOrEqualTo(100).WithMessage("Page size must not exceed 100.");

        RuleFor(x => x.Model)
            .MaximumLength(50).WithMessage("Model must not exceed 50 characters.");

        RuleFor(x => x.Manufacturer)
            .MaximumLength(50).WithMessage("Manufacturer must not exceed 50 characters.");
    }
}
