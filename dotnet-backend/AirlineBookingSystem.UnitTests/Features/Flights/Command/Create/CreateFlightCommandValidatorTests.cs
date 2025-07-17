using AirlineBookingSystem.Application.Features.Flights.Commands.Create;
using AirlineBookingSystem.Shared.DTOs.flights;
using FluentAssertions;
using Xunit;

namespace AirlineBookingSystem.UnitTests.Features.Flights.Command.Create;

public class CreateFlightCommandValidatorTests
{
    private readonly CreateFlightCommandValidator _validator;

    public CreateFlightCommandValidatorTests()
    {
        _validator = new CreateFlightCommandValidator();
    }

    [Fact]
    public void ShouldHaveError_WhenDepartureTimeIsEmpty()
    {
        // Arrange
        var dto = new CreateFlightDto
        {
            DepartureTime = default, // Empty DateTimeOffset
            ArrivalTime = DateTimeOffset.UtcNow.AddHours(3),
            AirplaneId = 1,
            DepartureGateId = 1
        };
        var command = new CreateFlightCommand(dto);

        // Act
        var result = _validator.Validate(command);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.PropertyName == "Dto.DepartureTime" && e.ErrorMessage == "Departure time is required.");
    }

    [Fact]
    public void ShouldHaveError_WhenDepartureTimeIsInThePast()
    {
        // Arrange
        var dto = new CreateFlightDto
        {
            DepartureTime = DateTimeOffset.UtcNow.AddHours(-1),
            ArrivalTime = DateTimeOffset.UtcNow.AddHours(3),
            AirplaneId = 1,
            DepartureGateId = 1
        };
        var command = new CreateFlightCommand(dto);

        // Act
        var result = _validator.Validate(command);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.PropertyName == "Dto.DepartureTime" && e.ErrorMessage == "Departure time must be in the future.");
    }

    [Fact]
    public void ShouldHaveError_WhenArrivalTimeIsBeforeDepartureTime()
    {
        // Arrange
        var dto = new CreateFlightDto
        {
            DepartureTime = DateTimeOffset.UtcNow.AddHours(3),
            ArrivalTime = DateTimeOffset.UtcNow.AddHours(1),
            AirplaneId = 1,
            DepartureGateId = 1
        };
        var command = new CreateFlightCommand(dto);

        // Act
        var result = _validator.Validate(command);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.PropertyName == "Dto.ArrivalTime" && e.ErrorMessage == "Arrival time must be after departure time.");
    }

    [Fact]
    public void ShouldHaveError_WhenAirplaneIdIsZeroOrLess()
    {
        // Arrange
        var dto = new CreateFlightDto
        {
            DepartureTime = DateTimeOffset.UtcNow.AddHours(1),
            ArrivalTime = DateTimeOffset.UtcNow.AddHours(3),
            AirplaneId = 0,
            DepartureGateId = 1
        };
        var command = new CreateFlightCommand(dto);

        // Act
        var result = _validator.Validate(command);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.PropertyName == "Dto.AirplaneId" && e.ErrorMessage == "Airplane ID must be greater than 0.");
    }

    [Fact]
    public void ShouldHaveError_WhenDepartureGateIdIsZeroOrLess()
    {
        // Arrange
        var dto = new CreateFlightDto
        {
            DepartureTime = DateTimeOffset.UtcNow.AddHours(1),
            ArrivalTime = DateTimeOffset.UtcNow.AddHours(3),
            AirplaneId = 1,
            DepartureGateId = 0
        };
        var command = new CreateFlightCommand(dto);

        // Act
        var result = _validator.Validate(command);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.PropertyName == "Dto.DepartureGateId" && e.ErrorMessage == "Departure gate ID must be greater than 0.");
    }

    [Fact]
    public void ShouldNotHaveError_WhenAllFieldsAreValid()
    {
        // Arrange
        var dto = new CreateFlightDto
        {
            DepartureTime = DateTimeOffset.UtcNow.AddHours(1),
            ArrivalTime = DateTimeOffset.UtcNow.AddHours(3),
            AirplaneId = 1,
            DepartureGateId = 1
        };
        var command = new CreateFlightCommand(dto);

        // Act
        var result = _validator.Validate(command);

        // Assert
        result.IsValid.Should().BeTrue();
    }
}
