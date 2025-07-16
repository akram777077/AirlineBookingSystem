using AirlineBookingSystem.Application.Features.Countries.Queries.GetAll;
using FluentAssertions;
using Xunit;

namespace AirlineBookingSystem.UnitTests.Features.Countries.Queries.GetAll;

public class GetAllCountriesQueryValidatorTests
{
    private readonly GetAllCountriesQueryValidator _validator;

    public GetAllCountriesQueryValidatorTests()
    {
        _validator = new GetAllCountriesQueryValidator();
    }

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
