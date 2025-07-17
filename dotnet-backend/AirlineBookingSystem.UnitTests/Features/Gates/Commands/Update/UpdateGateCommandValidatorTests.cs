using AirlineBookingSystem.Application.Features.Gates.Commands.Update;
using AirlineBookingSystem.Shared.DTOs.Gates;
using FluentAssertions;

namespace AirlineBookingSystem.UnitTests.Features.Gates.Commands.Update;

public class UpdateGateCommandValidatorTests
{
    private readonly UpdateGateCommandValidator _validator = new();

    [Fact]
    public void Should_HaveError_WhenIdIsEmpty()
    {
        // Arrange
        var command = new UpdateGateCommand(0, new UpdateGateDto { Id = 0, GateNumber = "G10", TerminalId = 1 });

        // Act
        var result = _validator.Validate(command);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle(e => e.PropertyName == "Id" && e.ErrorMessage == "Id is required.");
    }

    [Fact]
    public void Should_HaveError_WhenGateNumberIsEmpty()
    {
        // Arrange
        var command = new UpdateGateCommand(1, new UpdateGateDto { Id = 1, GateNumber = "", TerminalId = 1 });

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
        var command = new UpdateGateCommand(1, new UpdateGateDto { Id = 1, GateNumber = "G1234567890", TerminalId = 1 });

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
        var command = new UpdateGateCommand(1, new UpdateGateDto { Id = 1, GateNumber = "G10", TerminalId = 0 }); // Assuming 0 is considered empty/invalid

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
        var command = new UpdateGateCommand(1, new UpdateGateDto { Id = 1, GateNumber = "G10", TerminalId = 1 });

        // Act
        var result = _validator.Validate(command);

        // Assert
        result.IsValid.Should().BeTrue();
    }
}
