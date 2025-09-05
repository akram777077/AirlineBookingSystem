using AirlineBookingSystem.Application.Features.Seats.Queries.GetById;
using FluentValidation;

namespace AirlineBookingSystem.Application.Features.Seats.Queries.GetById;

/// <summary>
/// Validator for the <see cref="GetSeatByIdQuery"/>.
/// </summary>
public class GetSeatByIdQueryValidator : AbstractValidator<GetSeatByIdQuery>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="GetSeatByIdQueryValidator"/> class.
    /// </summary>
    public GetSeatByIdQueryValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Seat ID is required.");
    }
}