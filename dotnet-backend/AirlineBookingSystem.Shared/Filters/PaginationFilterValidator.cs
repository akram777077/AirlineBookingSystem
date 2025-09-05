using FluentValidation;

namespace AirlineBookingSystem.Shared.Filters;

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
