using AirlineBookingSystem.Application.Features.Seats.Commands.Create;
using AirlineBookingSystem.Shared.DTOs;
using FluentAssertions;
using FluentValidation.TestHelper;

namespace AirlineBookingSystem.UnitTests.Features.Seats.Commands.Create;

public class CreateSeatCommandValidatorTests
{
    private readonly CreateSeatCommandValidator _validator = new();

    [Fact]
    public void Should_HaveError_WhenClassTypesIdIsZero()
    {
        // Arrange
        var command = new CreateSeatCommand(new CreateSeatDto { ClassTypesId = 0, SeatNumber = "1A", AirplaneId = 1 });

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Seat.ClassTypesId);
    }

    [Fact]
    public void Should_HaveError_WhenSeatNumberIsEmpty()
    {
        // Arrange
        var command = new CreateSeatCommand(new CreateSeatDto { ClassTypesId = 1, SeatNumber = "", AirplaneId = 1 });

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Seat.SeatNumber);
    }

    [Fact]
    public void Should_HaveError_WhenSeatNumberExceedsMaxLength()
    {
        // Arrange
        var command = new CreateSeatCommand(new CreateSeatDto { ClassTypesId = 1, SeatNumber = "12345678901", AirplaneId = 1 });

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Seat.SeatNumber);
    }

    [Fact]
    public void Should_HaveError_WhenAirplaneIdIsZero()
    {
        // Arrange
        var command = new CreateSeatCommand(new CreateSeatDto { ClassTypesId = 1, SeatNumber = "1A", AirplaneId = 0 });

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Seat.AirplaneId);
    }

    [Fact]
    public void Should_NotHaveError_WhenCommandIsValid()
    {
        // Arrange
        var command = new CreateSeatCommand(new CreateSeatDto { ClassTypesId = 1, SeatNumber = "1A", AirplaneId = 1 });

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }
}