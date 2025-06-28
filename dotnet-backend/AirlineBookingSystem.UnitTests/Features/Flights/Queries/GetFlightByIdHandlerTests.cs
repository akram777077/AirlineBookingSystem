using AirlineBookingSystem.Application.Features.Flights.Queries.ById;
using AirlineBookingSystem.Application.Interfaces.Repositories;
using AirlineBookingSystem.Domain.Entities;
using AirlineBookingSystem.Shared.DTOs.Flights;
using AirlineBookingSystem.UnitTests.Common.TestData;
using AutoMapper;
using FluentAssertions;
using Moq;

namespace AirlineBookingSystem.UnitTests.Features.Flights.Queries;

public class GetFlightByIdHandlerTests
{
    [Fact]
    public async Task Handle_ShouldReturnFlight_WhenFlightExists()
    {
        // Arrange
        var flightId = 1;
        var flight = FlightFactory.Create();
        var flightDto = flight.ToDto();
        
        var flightRepositoryMock = new Mock<IFlightRepository>();
        flightRepositoryMock.Setup(repo => repo.GetByIdAsync(flightId))
            .ReturnsAsync(flight);

        var mapperMock = new Mock<IMapper>();
        mapperMock.Setup(m => m.Map<FlightDto>(flight))
            .Returns(flightDto);

        var handler = new GetFlightByIdHandler(flightRepositoryMock.Object, mapperMock.Object);
        var query = new GetFlightByIdQuery(flightId);

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        result.Should().BeEquivalentTo(flightDto);
    }
    [Fact]
    public async Task Handle_ShouldReturnNull_WhenFlightDoesNotExist()
    {
        // Arrange
        var flightId = 1;

        var flightRepositoryMock = new Mock<IFlightRepository>();
        flightRepositoryMock.Setup(repo => repo.GetByIdAsync(flightId))
            .ReturnsAsync((Flight?)null);

        var mapperMock = new Mock<IMapper>();

        var handler = new GetFlightByIdHandler(flightRepositoryMock.Object, mapperMock.Object);
        var query = new GetFlightByIdQuery(flightId);

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        result.Should().BeNull();
    }
}