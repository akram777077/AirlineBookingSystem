using AirlineBookingSystem.Application.Features.Flights.Commands.Create;
using AirlineBookingSystem.Application.Interfaces.Repositories;
using AirlineBookingSystem.Application.Interfaces.UnitOfWork;
using AirlineBookingSystem.Domain.Entities;
using AirlineBookingSystem.UnitTests.Common.TestData;
using Moq;
using Xunit;
using FluentAssertions;

namespace AirlineBookingSystem.UnitTests.Features.Flights.Commands;

public class CreateFlightCommandHandlerTests
{
    [Fact]
    public async Task Handle_ShouldCreateFlight_WhenDataIsValid()
    {
        // Arrange
        var fromAirport = AirportFactory.Create(1);
        var toAirport = AirportFactory.Create(2);
        var flightRepoMock = new Mock<IFlightRepository>();
        var airportRepoMock = new Mock<IAirportRepository>();
        var unitOfWorkMock = new Mock<IUnitOfWork>();

        airportRepoMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(fromAirport);
        airportRepoMock.Setup(r => r.GetByIdAsync(2)).ReturnsAsync(toAirport);
        unitOfWorkMock.Setup(u => u.Airports).Returns(airportRepoMock.Object);
        unitOfWorkMock.Setup(u => u.Flights).Returns(flightRepoMock.Object);
        unitOfWorkMock.Setup(u => u.CompleteAsync()).ReturnsAsync(1);

        var handler = new CreateFlightCommandHandler(unitOfWorkMock.Object);
        var command = new CreateFlightCommand(
            "FL123",
            1,
            2,
            DateTime.UtcNow.AddHours(1),
            DateTime.UtcNow.AddHours(5),
            100.0m
        );

        // Act
        var result = await handler.Handle(command, default);

        // Assert
        flightRepoMock.Verify(r => r.AddAsync(It.Is<Flight>(f =>
            f.FlightNumber == command.FlightNumber &&
            f.FromAirportId == command.FromAirportId &&
            f.ToAirportId == command.ToAirportId &&
            f.DepartureTime == command.DepartureTime &&
            f.ArrivalTime == command.ArrivalTime &&
            f.FromAirport.Id == fromAirport.Id &&
            f.ToAirport.Id == toAirport.Id
        )), Times.Once);
        unitOfWorkMock.Verify(u => u.CompleteAsync(), Times.Once);
        result.Should().Be(0); // Id is 0 because it's not set by the mock
    }

    [Fact]
    public async Task Handle_ShouldThrow_WhenAirportNotFound()
    {
        // Arrange
        var airportRepoMock = new Mock<IAirportRepository>();
        var unitOfWorkMock = new Mock<IUnitOfWork>();
        airportRepoMock.Setup(r => r.GetByIdAsync(It.IsAny<int>())).ReturnsAsync((Airport?)null);
        unitOfWorkMock.Setup(u => u.Airports).Returns(airportRepoMock.Object);
        var handler = new CreateFlightCommandHandler(unitOfWorkMock.Object);
        var command = new CreateFlightCommand("FL123", 1, 2, DateTime.UtcNow, DateTime.UtcNow.AddHours(2), 100);

        // Act & Assert
        await Assert.ThrowsAsync<InvalidOperationException>(() => handler.Handle(command, default));
    }
}
