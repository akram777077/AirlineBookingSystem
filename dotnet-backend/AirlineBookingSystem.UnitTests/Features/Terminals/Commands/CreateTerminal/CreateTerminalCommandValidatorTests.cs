using AirlineBookingSystem.Application.Features.Terminals.Commands.Create;
using AirlineBookingSystem.Shared.DTOs.terminals;
using FluentAssertions;
using Xunit;

namespace AirlineBookingSystem.UnitTests.Features.Terminals.Commands.CreateTerminal;

public class CreateTerminalCommandValidatorTests
{
    private readonly CreateTerminalCommandValidator _validator;

    public CreateTerminalCommandValidatorTests()
    {
        _validator = new CreateTerminalCommandValidator();
    }

    [Fact]
    public void ShouldHaveError_WhenNameIsEmpty()
    {
        // Arrange
        var dto = new CreateTerminalDto("", 1);
        var command = new CreateTerminalCommand(dto);

        // Act
        var result = _validator.Validate(command);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.PropertyName == "Dto.Name" && e.ErrorMessage == "Terminal name is required.");
    }

    [Fact]
    public void ShouldHaveError_WhenNameExceedsMaxLength()
    {
        // Arrange
        var longName = new string('A', 101);
        var dto = new CreateTerminalDto(longName, 1);
        var command = new CreateTerminalCommand(dto);

        // Act
        var result = _validator.Validate(command);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.PropertyName == "Dto.Name" && e.ErrorMessage == "Terminal name cannot exceed 100 characters.");
    }

    [Fact]
    public void ShouldHaveError_WhenAirportIdIsZeroOrLess()
    {
        // Arrange
        var dto = new CreateTerminalDto("Terminal A", 0);
        var command = new CreateTerminalCommand(dto);

        // Act
        var result = _validator.Validate(command);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.PropertyName == "Dto.AirportId" && e.ErrorMessage == "Airport ID must be greater than 0.");
    }

    [Fact]
    public void ShouldNotHaveError_WhenAllFieldsAreValid()
    {
        // Arrange
        var dto = new CreateTerminalDto("Terminal A", 1);
        var command = new CreateTerminalCommand(dto);

        // Act
        var result = _validator.Validate(command);

        // Assert
        result.IsValid.Should().BeTrue();
    }
}
