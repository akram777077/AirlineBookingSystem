using AirlineBookingSystem.Application.Features.Seats.Queries.GetAvailableSeats;
using FluentValidation;

namespace AirlineBookingSystem.Application.Features.Seats.Queries.GetAvailableSeats;

/// <summary>
/// Validator for the <see cref="GetAvailableSeatsQuery"/>.
/// </summary>
public class GetAvailableSeatsQueryValidator : AbstractValidator<GetAvailableSeatsQuery>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="GetAvailableSeatsQueryValidator"/> class.
    /// </summary>
    public GetAvailableSeatsQueryValidator()
    {
        RuleFor(x => x.Filter.FlightId)
            .NotEmpty().WithMessage("Flight ID is required.");
    }
}