using AirlineBookingSystem.Application.Features.Seats.Queries.GetAvailableSeats;
using FluentValidation;

namespace AirlineBookingSystem.Application.Features.Seats.Queries.GetAvailableSeats;

public class GetAvailableSeatsQueryValidator : AbstractValidator<GetAvailableSeatsQuery>
{
    public GetAvailableSeatsQueryValidator()
    {
        RuleFor(x => x.Filter.FlightId)
            .NotEmpty().WithMessage("Flight ID is required.");
    }
}