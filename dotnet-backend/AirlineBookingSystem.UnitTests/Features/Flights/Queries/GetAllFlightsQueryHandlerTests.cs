using AirlineBookingSystem.Application.Features.Flights.Queries.All;
using AirlineBookingSystem.Application.Interfaces.Services;
using AirlineBookingSystem.Domain.Entities;
using AirlineBookingSystem.Shared.DTOs.Flights;
using AirlineBookingSystem.UnitTests.Common.TestData;
using AutoMapper;
using FluentAssertions;
using Moq;

namespace AirlineBookingSystem.UnitTests.Features.Flights.Queries;

public class GetAllFlightsQueryHandlerTests
{
    [Fact]
    public async Task Handle_ShouldReturnAllFlights()
    {
        // Arrange
        var flightService = new Mock<IFlightService>();
        var mapper = new Mock<IMapper>();
        var flights = new List<Flight>
        {
            FlightFactory.Create(),
            FlightFactory.Create(2,"FL456")
        };
        var flightsDto = flights.Select(f => f.ToDto()).ToList();
        
        flightService.Setup(s => s.GetAllAsync())
            .ReturnsAsync(flights);
        
        mapper.Setup(m => m.Map<List<FlightDto>>(flights))
            .Returns(flightsDto);

        var handler = new GetAllFlightsQueryHandler(flightService.Object, mapper.Object);
        
        // Act
        var result = await handler.Handle(new GetAllFlightsQuery(), CancellationToken.None);
        var resultList = result.ToList();
        
        // Assert
        for (int i = 0; i < resultList.Count; i++)
            resultList[i].Should().BeEquivalentTo(flightsDto[i]);
    }
}