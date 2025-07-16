using AirlineBookingSystem.Application.Features.Flights.Command.MarkAsArrived;
using AirlineBookingSystem.Application.Interfaces.UnitOfWork;
using AirlineBookingSystem.Domain.Entities;
using AirlineBookingSystem.Shared.Enums;
using AirlineBookingSystem.UnitTests.Common.TestData;
using FluentAssertions;
using Moq;
using Xunit;

namespace AirlineBookingSystem.UnitTests.Features.Flights.Command.MarkAsArrived;

public class MarkFlightAsArrivedCommandValidatorTests
{
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly MarkFlightAsArrivedCommandValidator _validator;

    public MarkFlightAsArrivedCommandValidatorTests()
    {
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _validator = new MarkFlightAsArrivedCommandValidator(_unitOfWorkMock.Object);
    }

    [Fact]
    public async Task ShouldHaveError_WhenFlightIdIsEmpty()
    {
        // Arrange
        var command = new MarkFlightAsArrivedCommand(0);
        _unitOfWorkMock.Setup(u => u.Flights.GetByIdAsync(It.IsAny<int>()))
            .ReturnsAsync(FlightFactory.GetFlightFaker(1, 1, 1, 1).Generate());

        // Act
        var result = await _validator.ValidateAsync(command);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.PropertyName == "Id" && e.ErrorMessage == "Flight ID is required.");
    }

    [Fact]
    public async Task ShouldHaveError_WhenFlightIdIsZeroOrLess()
    {
        // Arrange
        var command = new MarkFlightAsArrivedCommand(-1);
        _unitOfWorkMock.Setup(u => u.Flights.GetByIdAsync(It.IsAny<int>()))
            .ReturnsAsync(FlightFactory.GetFlightFaker(1, 1, 1, 1).Generate());

        // Act
        var result = await _validator.ValidateAsync(command);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.PropertyName == "Id" && e.ErrorMessage == "Flight ID must be greater than zero.");
    }

    [Fact]
    public async Task ShouldHaveError_WhenFlightStatusIsNotDeparted()
    {
        // Arrange
        var flightId = 1;
        var command = new MarkFlightAsArrivedCommand(flightId);
        var flight = FlightFactory.GetFlightFaker(1, 1, 1, (int)FlightStatusEnum.Scheduled).Generate();
        flight.Id = flightId;
        flight.FlightStatus = new FlightStatus { Id = (int)FlightStatusEnum.Scheduled, StatusName = FlightStatusEnum.Scheduled };

        _unitOfWorkMock.Setup(u => u.Flights.GetByIdAsync(flightId))
            .ReturnsAsync(flight);

        // Act
        var result = await _validator.ValidateAsync(command);

        // Assert
        result.Errors.Should().Contain(e => e.ErrorMessage == "Flight must be departed to be marked as arrived.");
    }

    [Fact]
    public async Task ShouldNotHaveError_WhenFlightIdIsValidAndFlightStatusIsDeparted()
    {
        // Arrange
        var flightId = 1;
        var command = new MarkFlightAsArrivedCommand(flightId);
        var flight = FlightFactory.GetFlightFaker(1, 1, 1, (int)FlightStatusEnum.Departed).Generate();
        flight.Id = flightId;
        flight.FlightStatus = new FlightStatus { Id = (int)FlightStatusEnum.Departed, StatusName = FlightStatusEnum.Departed };

        _unitOfWorkMock.Setup(u => u.Flights.GetByIdAsync(flightId))
            .ReturnsAsync(flight);

        // Act
        var result = await _validator.ValidateAsync(command);

        // Assert
        result.IsValid.Should().BeTrue();
    }
}