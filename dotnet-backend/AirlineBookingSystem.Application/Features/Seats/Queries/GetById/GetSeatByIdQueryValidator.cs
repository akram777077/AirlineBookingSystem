using AirlineBookingSystem.Application.Features.Seats.Queries.GetById;
using FluentValidation;

namespace AirlineBookingSystem.Application.Features.Seats.Queries.GetById;

public class GetSeatByIdQueryValidator : AbstractValidator<GetSeatByIdQuery>
{
    public GetSeatByIdQueryValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Seat ID is required.");
    }
}