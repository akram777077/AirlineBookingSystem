using AirlineBookingSystem.Application.Features.Countries.Queries.GetAll;
using FluentAssertions;

namespace AirlineBookingSystem.UnitTests.Features.Countries.Queries.GetAll;

public class GetAllCountriesQueryValidatorTests
{
    private readonly GetAllCountriesQueryValidator _validator = new();

    [Fact]
    public void ShouldNotHaveError_WhenQueryIsValid()
    {
        // Arrange
        var query = new GetAllCountriesQuery();

        // Act
        var result = _validator.Validate(query);

        // Assert
        result.IsValid.Should().BeTrue();
    }
}
