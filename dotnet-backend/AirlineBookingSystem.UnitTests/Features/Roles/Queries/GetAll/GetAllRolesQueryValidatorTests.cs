using AirlineBookingSystem.Application.Features.Roles.Queries.GetAll;
using FluentAssertions;

namespace AirlineBookingSystem.UnitTests.Features.Roles.Queries.GetAll;

public class GetAllRolesQueryValidatorTests
{
    private readonly GetAllRolesQueryValidator _validator = new();

    [Fact]
    public void ShouldNotHaveError_WhenQueryIsValid()
    {
        // Arrange
        var query = new GetAllRolesQuery();

        // Act
        var result = _validator.Validate(query);

        // Assert
        result.IsValid.Should().BeTrue();
    }
}
