using AirlineBookingSystem.Application.Features.ClassTypes.Queries.GetById;
using FluentAssertions;
using Xunit;

namespace AirlineBookingSystem.UnitTests.Features.ClassTypes.Queries.GetById;

public class GetClassTypeByIdQueryValidatorTests
{
    private readonly GetClassTypeByIdQueryValidator _validator;

    public GetClassTypeByIdQueryValidatorTests()
    {
        _validator = new GetClassTypeByIdQueryValidator();
    }

    [Fact]
    public void ShouldHaveError_WhenClassTypeIdIsZeroOrLess()
    {
        // Arrange
        var query = new GetClassTypeByIdQuery(0);

        // Act
        var result = _validator.Validate(query);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.PropertyName == "Id" && e.ErrorMessage == "Id must be greater than 0.");
    }

    [Fact]
    public void ShouldNotHaveError_WhenClassTypeIdIsValid()
    {
        // Arrange
        var query = new GetClassTypeByIdQuery(1);

        // Act
        var result = _validator.Validate(query);

        // Assert
        result.IsValid.Should().BeTrue();
    }
}
