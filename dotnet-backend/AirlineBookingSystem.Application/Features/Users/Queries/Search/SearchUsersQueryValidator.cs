using FluentValidation;

namespace AirlineBookingSystem.Application.Features.Users.Queries.Search;

public class SearchUsersQueryValidator : AbstractValidator<SearchUsersQuery>
{
    public SearchUsersQueryValidator()
    {
        RuleFor(x => x.Filter).NotNull().WithMessage("User search filter cannot be null.");

        When(x => x.Filter != null, () =>
        {
            RuleFor(x => x.Filter.PageNumber)
                .GreaterThan(0).WithMessage("Page number must be greater than 0.");

            RuleFor(x => x.Filter.PageSize)
                .GreaterThan(0).WithMessage("Page size must be greater than 0.")
                .LessThanOrEqualTo(100).WithMessage("Page size cannot exceed 100.");
        });
    }
}
