using AirlineBookingSystem.Application.Features.Users.Queries.Search;
using FluentValidation.TestHelper;
using AirlineBookingSystem.Shared.Filters;

namespace AirlineBookingSystem.UnitTests.Features.Users.Queries;

public class SearchUsersQueryValidatorTests
{
    private readonly SearchUsersQueryValidator _validator;

    public SearchUsersQueryValidatorTests()
    {
        _validator = new SearchUsersQueryValidator();
    }

    [Fact]
    public void Should_Have_Error_When_PageNumberIsZero()
    {
        var query = new SearchUsersQuery(new UserSearchFilter { PageNumber = 0, PageSize = 10 });
        var result = _validator.TestValidate(query);
        result.ShouldHaveValidationErrorFor(x => x.Filter.PageNumber)
            .WithErrorMessage("Page number must be greater than 0.");
    }

    [Fact]
    public void Should_Have_Error_When_PageNumberIsNegative()
    {
        var query = new SearchUsersQuery(new UserSearchFilter { PageNumber = -1, PageSize = 10 });
        var result = _validator.TestValidate(query);
        result.ShouldHaveValidationErrorFor(x => x.Filter.PageNumber)
            .WithErrorMessage("Page number must be greater than 0.");
    }

    [Fact]
    public void Should_Have_Error_When_PageSizeIsZero()
    {
        var query = new SearchUsersQuery(new UserSearchFilter { PageNumber = 1, PageSize = 0 });
        var result = _validator.TestValidate(query);
        result.ShouldHaveValidationErrorFor(x => x.Filter.PageSize)
            .WithErrorMessage("Page size must be greater than 0.");
    }

    [Fact]
    public void Should_Have_Error_When_PageSizeIsNegative()
    {
        var query = new SearchUsersQuery(new UserSearchFilter { PageNumber = 1, PageSize = -1 });
        var result = _validator.TestValidate(query);
        result.ShouldHaveValidationErrorFor(x => x.Filter.PageSize)
            .WithErrorMessage("Page size must be greater than 0.");
    }

    [Fact]
    public void Should_Have_Error_When_PageSizeExceeds100()
    {
        var query = new SearchUsersQuery(new UserSearchFilter { PageNumber = 1, PageSize = 101 });
        var result = _validator.TestValidate(query);
        result.ShouldHaveValidationErrorFor(x => x.Filter.PageSize)
            .WithErrorMessage("Page size cannot exceed 100.");
    }

    [Fact]
    public void Should_Not_Have_Error_When_ValidQuery()
    {
        var query = new SearchUsersQuery(new UserSearchFilter { PageNumber = 1, PageSize = 10, Username = "test", Email = "test@example.com", IsActive = true, RoleId = 1 });
        var result = _validator.TestValidate(query);
        result.ShouldNotHaveAnyValidationErrors();
    }
}
