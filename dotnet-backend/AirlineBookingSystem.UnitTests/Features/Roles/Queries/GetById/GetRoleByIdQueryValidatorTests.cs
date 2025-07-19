using AirlineBookingSystem.Application.Features.Roles.Queries.GetById;
using FluentAssertions;

namespace AirlineBookingSystem.UnitTests.Features.Roles.Queries.GetById;

public class GetRoleByIdQueryValidatorTests
{
    private readonly GetRoleByIdQueryValidator _validator = new();

    [Fact]
    public void ShouldHaveError_WhenRoleIdIsZeroOrLess()
    {
        // Arrange
        var query = new GetRoleByIdQuery(0);

        // Act
        var result = _validator.Validate(query);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.PropertyName == "Id" && e.ErrorMessage == "Id must be greater than 0.");
    }

    [Fact]
    public void ShouldNotHaveError_WhenRoleIdIsValid()
    {
        // Arrange
        var query = new GetRoleByIdQuery(1);

        // Act
        var result = _validator.Validate(query);

        // Assert
        result.IsValid.Should().BeTrue();
    }
}
