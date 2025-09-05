using FluentValidation;

namespace AirlineBookingSystem.Application.Features.Gates.Queries.Search;

/// <summary>
/// Validator for the <see cref="SearchGatesQuery"/>.
/// </summary>
public class SearchGatesQueryValidator : AbstractValidator<SearchGatesQuery>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="SearchGatesQueryValidator"/> class.
    /// </summary>
    public SearchGatesQueryValidator()
    {
        RuleFor(x => x.Filter.PageNumber)
            .GreaterThanOrEqualTo(1).WithMessage("Page number must be at least 1.");

        RuleFor(x => x.Filter.PageSize)
            .GreaterThanOrEqualTo(1).WithMessage("Page size must be at least 1.")
            .LessThanOrEqualTo(100).WithMessage("Page size cannot exceed 100.");

        When(x => !string.IsNullOrWhiteSpace(x.Filter.GateNumber),
            () =>
            {
                RuleFor(x => x.Filter.GateNumber)
                    .MaximumLength(10).WithMessage("Gate number cannot exceed 10 characters.");
            });
    }
}
