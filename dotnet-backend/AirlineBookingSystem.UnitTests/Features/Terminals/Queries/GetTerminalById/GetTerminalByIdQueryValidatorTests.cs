using AirlineBookingSystem.Application.Features.Terminals.Queries.GetById;
using FluentAssertions;
using Xunit;

namespace AirlineBookingSystem.UnitTests.Features.Terminals.Queries.GetTerminalById;

public class GetTerminalByIdQueryValidatorTests
{
    private readonly GetTerminalByIdQueryValidator _validator;

    public GetTerminalByIdQueryValidatorTests()
    {
        _validator = new GetTerminalByIdQueryValidator();
    }

    [Fact]
    public void ShouldHaveError_WhenIdIsZeroOrLess()
    {
        // Arrange
        var query = new GetTerminalByIdQuery(0);

        // Act
        var result = _validator.Validate(query);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.PropertyName == "Id" && e.ErrorMessage == "Terminal ID must be greater than 0.");
    }

    [Fact]
    public void ShouldNotHaveError_WhenIdIsValid()
    {
        // Arrange
        var query = new GetTerminalByIdQuery(1);

        // Act
        var result = _validator.Validate(query);

        // Assert
        result.IsValid.Should().BeTrue();
    }
}
