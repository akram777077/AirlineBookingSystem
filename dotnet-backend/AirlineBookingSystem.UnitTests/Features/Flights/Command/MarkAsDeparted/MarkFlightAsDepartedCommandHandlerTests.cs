using AirlineBookingSystem.Application.Features.Flights.Command.MarkAsDeparted;
using AirlineBookingSystem.Application.Interfaces.UnitOfWork;
using AirlineBookingSystem.Domain.Entities;
using AirlineBookingSystem.Shared.Enums;
using AirlineBookingSystem.Shared.Results;
using AirlineBookingSystem.UnitTests.Common.TestData;
using FluentAssertions;
using Moq;
using Xunit;

namespace AirlineBookingSystem.UnitTests.Features.Flights.Command.MarkAsDeparted;

public class MarkFlightAsDepartedCommandHandlerTests
{
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly MarkFlightAsDepartedCommandHandler _handler;

    public MarkFlightAsDepartedCommandHandlerTests()
    {
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _handler = new MarkFlightAsDepartedCommandHandler(_unitOfWorkMock.Object);
    }

    [Fact]
    public async Task Handle_ShouldReturnSuccess_WhenFlightIsMarkedAsDeparted()
    {
        // Arrange
        var flightId = 1;
        var command = new MarkFlightAsDepartedCommand(flightId);
        var flight = FlightFactory.GetFlightFaker(1, 1, 1, (int)FlightStatusEnum.Scheduled).Generate();
        flight.Id = flightId;

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
        result.StatusCode.Should().Be(ResultStatusCode.NoContent);
        flight.FlightStatusId.Should().Be((int)FlightStatusEnum.Departed);
        _unitOfWorkMock.Verify(u => u.Flights.Update(flight), Times.Once);
        _unitOfWorkMock.Verify(u => u.CompleteAsync(), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldReturnNotFound_WhenFlightDoesNotExist()
    {
        // Arrange
        var flightId = 1;
        var command = new MarkFlightAsDepartedCommand(flightId);

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
}