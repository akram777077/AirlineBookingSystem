using AirlineBookingSystem.Shared.Filters;
using FluentValidation;

namespace AirlineBookingSystem.Application.Features.Airplanes.Queries.Search;

public class SearchAirplanesQueryValidator : AbstractValidator<SearchAirplanesQuery>
{
    public SearchAirplanesQueryValidator()
    {
        RuleFor(x => x.Filter).SetValidator(new PaginationFilterValidator());

        RuleFor(x => x.Filter.Model)
            .MaximumLength(100).WithMessage("Model cannot exceed 100 characters.")
            .When(x => !string.IsNullOrWhiteSpace(x.Filter.Model));

        RuleFor(x => x.Filter.Manufacturer)
            .MaximumLength(100).WithMessage("Manufacturer cannot exceed 100 characters.")
            .When(x => !string.IsNullOrWhiteSpace(x.Filter.Manufacturer));
    }
}
