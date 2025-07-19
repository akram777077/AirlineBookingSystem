using AirlineBookingSystem.Application.Features.Cities.Queries.GetById;
using FluentAssertions;

namespace AirlineBookingSystem.UnitTests.Features.Cities.Queries.GetById;

public class GetCityByIdQueryValidatorTests
{
    private readonly GetCityByIdQueryValidator _validator = new();

    [Fact]
    public void ShouldHaveError_WhenCityIdIsZeroOrLess()
    {
        // Arrange
        var query = new GetCityByIdQuery(0);

        // Act
        var result = _validator.Validate(query);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.PropertyName == "Id" && e.ErrorMessage == "City ID must be greater than 0.");
    }

    [Fact]
    public void ShouldNotHaveError_WhenCityIdIsValid()
    {
        // Arrange
        var query = new GetCityByIdQuery(1);

        // Act
        var result = _validator.Validate(query);

        // Assert
        result.IsValid.Should().BeTrue();
    }
}