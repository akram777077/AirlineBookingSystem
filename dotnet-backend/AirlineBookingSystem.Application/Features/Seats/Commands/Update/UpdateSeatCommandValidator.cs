using AirlineBookingSystem.Application.Features.Seats.Commands.Update;
using FluentValidation;

namespace AirlineBookingSystem.Application.Features.Seats.Commands.Update;

/// <summary>
/// Validator for the <see cref="UpdateSeatCommand"/>.
/// </summary>
public class UpdateSeatCommandValidator : AbstractValidator<UpdateSeatCommand>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="UpdateSeatCommandValidator"/> class.
    /// </summary>
    public UpdateSeatCommandValidator()
    {
        RuleFor(x => x.Seat.ClassTypesId)
            .NotEmpty().WithMessage("Class Type ID is required.");

        RuleFor(x => x.Seat.SeatNumber)
            .NotEmpty().WithMessage("Seat Number is required.")
            .MaximumLength(10).WithMessage("Seat Number cannot exceed 10 characters.");

        RuleFor(x => x.Seat.AirplaneId)
            .NotEmpty().WithMessage("Airplane ID is required.");
    }
}