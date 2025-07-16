using FluentValidation;

namespace AirlineBookingSystem.Application.Features.Countries.Queries.GetAll;

public class GetAllCountriesQueryValidator : AbstractValidator<GetAllCountriesQuery>
{
    public GetAllCountriesQueryValidator()
    {
    }
}
