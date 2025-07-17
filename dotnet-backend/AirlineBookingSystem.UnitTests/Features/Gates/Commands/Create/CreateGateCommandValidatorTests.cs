using AirlineBookingSystem.Application.Features.Gates.Commands.Create;
using AirlineBookingSystem.Shared.DTOs.Gates;
using FluentAssertions;

namespace AirlineBookingSystem.UnitTests.Features.Gates.Commands.Create;

public class CreateGateCommandValidatorTests
{
    private readonly CreateGateCommandValidator _validator = new();

    [Fact]
    public void Should_HaveError_WhenGateNumberIsEmpty()
    {
        // Arrange
        var command = new CreateGateCommand(new CreateGateDto { GateNumber = "", TerminalId = 1 });

        // Act
        var result = _validator.Validate(command);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle(e => e.PropertyName == "Dto.GateNumber" && e.ErrorMessage == "Gate number is required.");
    }

    [Fact]
    public void Should_HaveError_WhenGateNumberExceedsMaxLength()
    {
        // Arrange
        var command = new CreateGateCommand(new CreateGateDto { GateNumber = "G1234567890", TerminalId = 1 });

        // Act
        var result = _validator.Validate(command);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle(e => e.PropertyName == "Dto.GateNumber" && e.ErrorMessage == "Gate number cannot exceed 10 characters.");
    }

    [Fact]
    public void Should_HaveError_WhenTerminalIdIsEmpty()
    {
        // Arrange
        var command = new CreateGateCommand(new CreateGateDto { GateNumber = "G10", TerminalId = 0 }); // Assuming 0 is considered empty/invalid

        // Act
        var result = _validator.Validate(command);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle(e => e.PropertyName == "Dto.TerminalId" && e.ErrorMessage == "Terminal ID is required.");
    }

    [Fact]
    public void Should_NotHaveError_WhenValidData()
    {
        // Arrange
        var command = new CreateGateCommand(new CreateGateDto { GateNumber = "G10", TerminalId = 1 });

        // Act
        var result = _validator.Validate(command);

        // Assert
        result.IsValid.Should().BeTrue();
    }
}
