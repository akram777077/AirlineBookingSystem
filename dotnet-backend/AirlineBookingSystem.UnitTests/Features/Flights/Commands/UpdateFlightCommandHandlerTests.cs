using AirlineBookingSystem.Application.Features.Flights.Commands.Update;
using AirlineBookingSystem.Application.Interfaces.Repositories;
using AirlineBookingSystem.Application.Interfaces.UnitOfWork;
using AirlineBookingSystem.Domain.Entities;
using AirlineBookingSystem.UnitTests.Common.TestData;
using Moq;
using Xunit;
using FluentAssertions;

namespace AirlineBookingSystem.UnitTests.Features.Flights.Commands;

public class UpdateFlightCommandHandlerTests
{
    [Fact]
    public async Task Handle_ShouldUpdateFlight_WhenDataIsValid()
    {
        // Arrange
        var flight = FlightFactory.Create(1);
        var fromAirport = AirportFactory.Create(1);
        var toAirport = AirportFactory.Create(2);
        var flightRepoMock = new Mock<IFlightRepository>();
        var airportRepoMock = new Mock<IAirportRepository>();
        var unitOfWorkMock = new Mock<IUnitOfWork>();

        flightRepoMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(flight);
        airportRepoMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(fromAirport);
        airportRepoMock.Setup(r => r.GetByIdAsync(2)).ReturnsAsync(toAirport);
        unitOfWorkMock.Setup(u => u.Flights).Returns(flightRepoMock.Object);
        unitOfWorkMock.Setup(u => u.Airports).Returns(airportRepoMock.Object);
        unitOfWorkMock.Setup(u => u.CompleteAsync()).ReturnsAsync(1);

        var handler = new UpdateFlightCommandHandler(unitOfWorkMock.Object);
        var command = new UpdateFlightCommand(
            1,
            "FL999",
            1,
            2,
            DateTime.UtcNow.AddHours(3),
            DateTime.UtcNow.AddHours(7)
        );

        // Act
        var result = await handler.Handle(command, default);

        // Assert
        result.Should().BeTrue();
        flight.FlightNumber.Should().Be(command.FlightNumber);
        flight.FromAirportId.Should().Be(command.FromAirportId);
        flight.ToAirportId.Should().Be(command.ToAirportId);
        flight.DepartureTime.Should().Be(command.DepartureTime);
        flight.ArrivalTime.Should().Be(command.ArrivalTime);
        flight.FromAirport.Id.Should().Be(fromAirport.Id);
        flight.ToAirport.Id.Should().Be(toAirport.Id);
        unitOfWorkMock.Verify(u => u.CompleteAsync(), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldReturnFalse_WhenFlightNotFound()
    {
        // Arrange
        var flightRepoMock = new Mock<IFlightRepository>();
        var unitOfWorkMock = new Mock<IUnitOfWork>();
        flightRepoMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync((Flight?)null);
        unitOfWorkMock.Setup(u => u.Flights).Returns(flightRepoMock.Object);
        var handler = new UpdateFlightCommandHandler(unitOfWorkMock.Object);
        var command = new UpdateFlightCommand(1, "FL999", 1, 2, DateTime.UtcNow, DateTime.UtcNow.AddHours(2));

        // Act
        var result = await handler.Handle(command, default);

        // Assert
        result.Should().BeFalse();
    }

    [Fact]
    public async Task Handle_ShouldThrow_WhenAirportNotFound()
    {
        // Arrange
        var flight = FlightFactory.Create(1);
        var flightRepoMock = new Mock<IFlightRepository>();
        var airportRepoMock = new Mock<IAirportRepository>();
        var unitOfWorkMock = new Mock<IUnitOfWork>();
        flightRepoMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(flight);
        airportRepoMock.Setup(r => r.GetByIdAsync(It.IsAny<int>())).ReturnsAsync((Airport?)null);
        unitOfWorkMock.Setup(u => u.Flights).Returns(flightRepoMock.Object);
        unitOfWorkMock.Setup(u => u.Airports).Returns(airportRepoMock.Object);
        var handler = new UpdateFlightCommandHandler(unitOfWorkMock.Object);
        var command = new UpdateFlightCommand(1, "FL999", 1, 2, DateTime.UtcNow, DateTime.UtcNow.AddHours(2));

        // Act & Assert
        await Assert.ThrowsAsync<InvalidOperationException>(() => handler.Handle(command, default));
    }
}
