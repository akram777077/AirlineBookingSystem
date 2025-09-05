using FluentValidation;

namespace AirlineBookingSystem.Application.Features.Terminals.Queries.GetById;

/// <summary>
/// Validator for the <see cref="GetTerminalByIdQuery"/>.
/// </summary>
public class GetTerminalByIdQueryValidator : AbstractValidator<GetTerminalByIdQuery>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="GetTerminalByIdQueryValidator"/> class.
    /// </summary>
    public GetTerminalByIdQueryValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("Terminal ID must be greater than 0.");
    }
}
