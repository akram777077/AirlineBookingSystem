using FluentValidation;

namespace AirlineBookingSystem.Application.Features.Airports.Query.Search;

public class SearchAirportsQueryValidator : AbstractValidator<SearchAirportsQuery>
{
    public SearchAirportsQueryValidator()
    {
        RuleFor(x => x.Filter.PageNumber)
            .GreaterThanOrEqualTo(1).WithMessage("PageNumber at least greater than or equal to 1.");

        RuleFor(x => x.Filter.PageSize)
            .GreaterThanOrEqualTo(1).WithMessage("PageSize at least greater than or equal to 1.");
    }
}