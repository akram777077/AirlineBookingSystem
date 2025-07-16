using FluentValidation;

namespace AirlineBookingSystem.Application.Features.Terminals.Queries.GetTerminalById;

public class GetTerminalByIdQueryValidator : AbstractValidator<GetTerminalByIdQuery>
{
    public GetTerminalByIdQueryValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("Terminal ID must be greater than 0.");
    }
}
