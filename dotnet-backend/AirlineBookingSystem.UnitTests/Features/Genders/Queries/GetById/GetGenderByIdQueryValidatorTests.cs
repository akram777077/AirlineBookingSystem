using AirlineBookingSystem.Application.Features.Genders.Queries.GetById;
using FluentAssertions;
using Xunit;

namespace AirlineBookingSystem.UnitTests.Features.Genders.Queries.GetById;

public class GetGenderByIdQueryValidatorTests
{
    private readonly GetGenderByIdQueryValidator _validator;

    public GetGenderByIdQueryValidatorTests()
    {
        _validator = new GetGenderByIdQueryValidator();
    }

    [Fact]
    public void ShouldHaveError_WhenGenderIdIsZeroOrLess()
    {
        // Arrange
        var query = new GetGenderByIdQuery(0);

        // Act
        var result = _validator.Validate(query);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.PropertyName == "Id" && e.ErrorMessage == "Id must be greater than 0.");
    }

    [Fact]
    public void ShouldNotHaveError_WhenGenderIdIsValid()
    {
        // Arrange
        var query = new GetGenderByIdQuery(1);

        // Act
        var result = _validator.Validate(query);

        // Assert
        result.IsValid.Should().BeTrue();
    }
}
