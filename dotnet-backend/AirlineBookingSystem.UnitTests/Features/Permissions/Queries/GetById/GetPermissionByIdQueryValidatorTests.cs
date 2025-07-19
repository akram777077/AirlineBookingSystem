using AirlineBookingSystem.Application.Features.Permissions.Queries.GetById;
using FluentAssertions;

namespace AirlineBookingSystem.UnitTests.Features.Permissions.Queries.GetById;

public class GetPermissionByIdQueryValidatorTests
{
    private readonly GetPermissionByIdQueryValidator _validator = new();

    [Fact]
    public void ShouldHaveError_WhenPermissionIdIsZeroOrLess()
    {
        // Arrange
        var query = new GetPermissionByIdQuery(0);

        // Act
        var result = _validator.Validate(query);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.PropertyName == "Id" && e.ErrorMessage == "Id must be greater than 0.");
    }

    [Fact]
    public void ShouldNotHaveError_WhenPermissionIdIsValid()
    {
        // Arrange
        var query = new GetPermissionByIdQuery(1);

        // Act
        var result = _validator.Validate(query);

        // Assert
        result.IsValid.Should().BeTrue();
    }
}
