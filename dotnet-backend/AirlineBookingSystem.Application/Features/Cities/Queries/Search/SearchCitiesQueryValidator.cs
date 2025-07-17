using FluentValidation;

namespace AirlineBookingSystem.Application.Features.Cities.Queries.Search;

public class SearchCitiesQueryValidator : AbstractValidator<SearchCitiesQuery>
{
    public SearchCitiesQueryValidator()
    {
        RuleFor(x => x.Filter).NotNull().WithMessage("City search filter cannot be null.");

        When(x => x.Filter is not null, () =>
        {
            RuleFor(x => x.Filter.PageNumber)
                .GreaterThanOrEqualTo(1).WithMessage("Page number must be at least 1.");

            RuleFor(x => x.Filter.PageSize)
                .GreaterThanOrEqualTo(1).WithMessage("Page size must be at least 1.");

            RuleFor(x => x.Filter.CountryId)
                .GreaterThan(0).When(x => x.Filter.CountryId.HasValue)
                .WithMessage("Country ID must be greater than zero.");

            RuleFor(x => x.Filter.Name)
                .MaximumLength(100).WithMessage("City name cannot exceed 100 characters.");
        });
    }
}