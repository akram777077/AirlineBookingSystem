using AirlineBookingSystem.Shared.Filters;
using FluentValidation;

namespace AirlineBookingSystem.Application.Features.Terminals.Queries.Search;

/// <summary>
/// Validator for the <see cref="SearchTerminalsQuery"/>.
/// </summary>
public class SearchTerminalsQueryValidator : AbstractValidator<SearchTerminalsQuery>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="SearchTerminalsQueryValidator"/> class.
    /// </summary>
    public SearchTerminalsQueryValidator()
    {
        RuleFor(x => x.Filter).SetValidator(new PaginationFilterValidator());

        RuleFor(x => x.Filter.AirportId)
            .GreaterThan(0).WithMessage("Airport ID must be greater than 0.")
            .When(x => x.Filter.AirportId.HasValue);

        RuleFor(x => x.Filter.Name)
            .MaximumLength(100).WithMessage("Terminal name cannot exceed 100 characters.")
            .When(x => !string.IsNullOrWhiteSpace(x.Filter.Name));
    }
}

/// <summary>
/// Validator for the <see cref="PaginationFilter"/>.
/// </summary>
public class PaginationFilterValidator : AbstractValidator<PaginationFilter>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PaginationFilterValidator"/> class.
    /// </summary>
    public PaginationFilterValidator()
    {
        RuleFor(x => x.PageNumber)
            .GreaterThanOrEqualTo(1).WithMessage("Page number must be at least 1.");

        RuleFor(x => x.PageSize)
            .GreaterThanOrEqualTo(1).WithMessage("Page size must be at least 1.")
            .LessThanOrEqualTo(100).WithMessage("Page size cannot exceed 100.");
    }
}