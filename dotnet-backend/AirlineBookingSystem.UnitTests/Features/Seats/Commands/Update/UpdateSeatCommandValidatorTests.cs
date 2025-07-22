using AirlineBookingSystem.Application.Features.Seats.Commands.Update;
using AirlineBookingSystem.Shared.DTOs.Seats;
using FluentAssertions;
using FluentValidation.TestHelper;

namespace AirlineBookingSystem.UnitTests.Features.Seats.Commands.Update;

public class UpdateSeatCommandValidatorTests
{
    private readonly UpdateSeatCommandValidator _validator = new();

    [Fact]
    public void Should_HaveError_WhenClassTypesIdIsZero()
    {
        // Arrange
        var command = new UpdateSeatCommand(1, new UpdateSeatDto { ClassTypesId = 0, SeatNumber = "1A", AirplaneId = 1 });

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Seat.ClassTypesId);
    }

    [Fact]
    public void Should_HaveError_WhenSeatNumberIsEmpty()
    {
        // Arrange
        var command = new UpdateSeatCommand(1, new UpdateSeatDto { ClassTypesId = 1, SeatNumber = "", AirplaneId = 1 });

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Seat.SeatNumber);
    }

    [Fact]
    public void Should_HaveError_WhenSeatNumberExceedsMaxLength()
    {
        // Arrange
        var command = new UpdateSeatCommand(1, new UpdateSeatDto { ClassTypesId = 1, SeatNumber = "12345678901", AirplaneId = 1 });

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Seat.SeatNumber);
    }

    [Fact]
    public void Should_HaveError_WhenAirplaneIdIsZero()
    {
        // Arrange
        var command = new UpdateSeatCommand(1, new UpdateSeatDto { ClassTypesId = 1, SeatNumber = "1A", AirplaneId = 0 });

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Seat.AirplaneId);
    }

    [Fact]
    public void Should_NotHaveError_WhenCommandIsValid()
    {
        // Arrange
        var command = new UpdateSeatCommand(1, new UpdateSeatDto { ClassTypesId = 1, SeatNumber = "1A", AirplaneId = 1 });

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }
}