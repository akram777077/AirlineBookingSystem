using AirlineBookingSystem.Application.Features.ClassTypes.Queries.GetAll;
using FluentAssertions;
using Xunit;

namespace AirlineBookingSystem.UnitTests.Features.ClassTypes.Queries.GetAll;

public class GetAllClassTypesQueryValidatorTests
{
    private readonly GetAllClassTypesQueryValidator _validator = new();

    [Fact]
    public void ShouldNotHaveError_WhenQueryIsValid()
    {
        // Arrange
        var query = new GetAllClassTypesQuery();

        // Act
        var result = _validator.Validate(query);

        // Assert
        result.IsValid.Should().BeTrue();
    }
}
