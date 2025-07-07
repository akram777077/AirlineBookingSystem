using FluentValidation;

namespace AirlineBookingSystem.Application.Features.Flights.Query.Search;

public class SearchFlightsQueryValidator : AbstractValidator<SearchFlightsQuery>
{
    public SearchFlightsQueryValidator()
    {
        RuleFor(x => x.Filter).NotNull().WithMessage("Flight search filter cannot be null.");

        When(x => x.Filter != null, () =>
        {
            RuleFor(x => x.Filter.DepartureDate)
                .Must(date => !date.HasValue || date.Value.Date >= DateTime.UtcNow.Date)
                .WithMessage("Departure date cannot be in the past.");

            RuleFor(x => x.Filter.FromCityId)
                .GreaterThan(0).When(x => x.Filter.FromCityId.HasValue)
                .WithMessage("From City ID must be greater than zero.");

            RuleFor(x => x.Filter.ToCityId)
                .GreaterThan(0).When(x => x.Filter.ToCityId.HasValue)
                .WithMessage("To City ID must be greater than zero.");

            RuleFor(x => x.Filter.FromCountryId)
                .GreaterThan(0).When(x => x.Filter.FromCountryId.HasValue)
                .WithMessage("From Country ID must be greater than zero.");

            RuleFor(x => x.Filter.ToCountryId)
                .GreaterThan(0).When(x => x.Filter.ToCountryId.HasValue)
                .WithMessage("To Country ID must be greater than zero.");
        });
    }
}