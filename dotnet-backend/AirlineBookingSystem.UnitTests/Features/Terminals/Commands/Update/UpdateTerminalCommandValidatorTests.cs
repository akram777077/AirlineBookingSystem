using AirlineBookingSystem.Application.Features.Terminals.Commands.Update;
using AirlineBookingSystem.Shared.DTOs.terminals;
using FluentAssertions;

namespace AirlineBookingSystem.UnitTests.Features.Terminals.Commands.Update;

public class UpdateTerminalCommandValidatorTests
{
    private readonly UpdateTerminalCommandValidator _validator = new();

    [Fact]
    public void ShouldHaveError_WhenIdIsZeroOrLess()
    {
        // Arrange
        var dto = new UpdateTerminalDto { Id = 0, Name = "Terminal A", AirportId = 1 };
        var command = new UpdateTerminalCommand(dto);

        // Act
        var result = _validator.Validate(command);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.PropertyName == "Dto.Id" && e.ErrorMessage == "Terminal ID must be greater than 0.");
    }

    [Fact]
    public void ShouldHaveError_WhenNameIsEmpty()
    {
        // Arrange
        var dto = new UpdateTerminalDto { Id = 1, Name = "", AirportId = 1 };
        var command = new UpdateTerminalCommand(dto);

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
        var dto = new UpdateTerminalDto { Id = 1, Name = longName, AirportId = 1 };
        var command = new UpdateTerminalCommand(dto);

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
        var dto = new UpdateTerminalDto { Id = 1, Name = "Terminal A", AirportId = 0 };
        var command = new UpdateTerminalCommand(dto);

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
        var dto = new UpdateTerminalDto { Id = 1, Name = "Terminal A", AirportId = 1 };
        var command = new UpdateTerminalCommand(dto);

        // Act
        var result = _validator.Validate(command);

        // Assert
        result.IsValid.Should().BeTrue();
    }
}
