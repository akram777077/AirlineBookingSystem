using AirlineBookingSystem.Application.Features.Permissions.Queries.GetAll;
using FluentAssertions;
using Xunit;

namespace AirlineBookingSystem.UnitTests.Features.Permissions.Queries.GetAll;

public class GetAllPermissionsQueryValidatorTests
{
    private readonly GetAllPermissionsQueryValidator _validator = new();

    [Fact]
    public void ShouldNotHaveError_WhenQueryIsValid()
    {
        // Arrange
        var query = new GetAllPermissionsQuery();

        // Act
        var result = _validator.Validate(query);

        // Assert
        result.IsValid.Should().BeTrue();
    }
}
