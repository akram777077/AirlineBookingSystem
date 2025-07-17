using AirlineBookingSystem.Shared.Filters;
using FluentValidation;

namespace AirlineBookingSystem.Application.Features.Terminals.Queries.Search;

public class SearchTerminalsQueryValidator : AbstractValidator<SearchTerminalsQuery>
{
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