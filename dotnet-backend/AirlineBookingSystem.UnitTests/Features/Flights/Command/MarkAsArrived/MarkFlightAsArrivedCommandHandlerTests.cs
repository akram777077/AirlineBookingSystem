using AirlineBookingSystem.Application.Features.Flights.Command.MarkAsArrived;
using AirlineBookingSystem.Application.Interfaces.UnitOfWork;
using AirlineBookingSystem.Domain.Entities;
using AirlineBookingSystem.Shared.Enums;
using AirlineBookingSystem.Shared.Results;
using FluentAssertions;
using Moq;
using Xunit;

using AirlineBookingSystem.UnitTests.Common.TestData;

namespace AirlineBookingSystem.UnitTests.Features.Flights.Command.MarkAsArrived;

public class MarkFlightAsArrivedCommandHandlerTests
{
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly MarkFlightAsArrivedCommandHandler _handler;

    public MarkFlightAsArrivedCommandHandlerTests()
    {
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _handler = new MarkFlightAsArrivedCommandHandler(_unitOfWorkMock.Object);
    }

    [Fact]
    public async Task Handle_ShouldReturnSuccess_WhenFlightIsMarkedAsArrived()
    {
        // Arrange
        var flightId = 1;
        var command = new MarkFlightAsArrivedCommand(flightId);
        var flight = FlightFactory.GetFlightFaker(1, 1, 1, (int)FlightStatusEnum.Departed).Generate();
        flight.Id = flightId;
        flight.FlightStatusId = (int)FlightStatusEnum.Departed;
        flight.FlightStatus = new FlightStatus { Id = (int)FlightStatusEnum.Departed, StatusName = FlightStatusEnum.Departed };

        _unitOfWorkMock.Setup(u => u.Flights.GetByIdAsync(flightId))
            .ReturnsAsync(flight);
        _unitOfWorkMock.Setup(u => u.Flights.Update(flight));
        _unitOfWorkMock.Setup(u => u.CompleteAsync())
            .ReturnsAsync(1);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.IsSuccess.Should().BeTrue();
        flight.FlightStatusId.Should().Be((int)FlightStatusEnum.Arrived);
        _unitOfWorkMock.Verify(u => u.Flights.Update(flight), Times.Once);
        _unitOfWorkMock.Verify(u => u.CompleteAsync(), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldReturnNotFound_WhenFlightDoesNotExist()
    {
        // Arrange
        var flightId = 1;
        var command = new MarkFlightAsArrivedCommand(flightId);

        _unitOfWorkMock.Setup(u => u.Flights.GetByIdAsync(flightId))
            .ReturnsAsync((Flight)null);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.IsSuccess.Should().BeFalse();
        result.StatusCode.Should().Be(ResultStatusCode.NotFound);
        result.Error.Should().Be("Flight not found.");
        _unitOfWorkMock.Verify(u => u.Flights.Update(It.IsAny<Flight>()), Times.Never);
        _unitOfWorkMock.Verify(u => u.CompleteAsync(), Times.Never);
    }

    [Fact]
    public async Task Handle_ShouldReturnFailure_WhenFlightStatusIsNotDeparted()
    {
        // Arrange
        var flightId = 1;
        var command = new MarkFlightAsArrivedCommand(flightId);
        var flight = FlightFactory.GetFlightFaker(1, 1, 1, (int)FlightStatusEnum.Scheduled).Generate();
        flight.Id = flightId;
        flight.Id = flightId;

        _unitOfWorkMock.Setup(u => u.Flights.GetByIdAsync(flightId))
            .ReturnsAsync(flight);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Error.Should().Be("Flight must be departed to be marked as arrived.");
        _unitOfWorkMock.Verify(u => u.Flights.Update(It.IsAny<Flight>()), Times.Never);
        _unitOfWorkMock.Verify(u => u.CompleteAsync(), Times.Never);
    }
}