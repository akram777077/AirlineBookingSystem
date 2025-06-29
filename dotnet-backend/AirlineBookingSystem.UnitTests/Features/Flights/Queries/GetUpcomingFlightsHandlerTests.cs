using AirlineBookingSystem.Application.Features.Flights.Queries.GetByDate;
using AirlineBookingSystem.Application.Interfaces.Services;
using AirlineBookingSystem.Domain.Entities;
using AirlineBookingSystem.Shared.DTOs.Flights;
using AirlineBookingSystem.UnitTests.Common.TestData;
using AutoMapper;
using FluentAssertions;
using Moq;

namespace AirlineBookingSystem.UnitTests.Features.Flights.Queries;

public class GetUpcomingFlightsHandlerTests
{
    [Fact]
    public async Task Handle_ShouldReturnUpcomingFlights_WhenFlightsExist()
    {
        // Arrange
        var mockFlightService = new Mock<IFlightService>();
        var dateTime = DateTime.UtcNow.AddDays(3);
        var flightList = new List<Flight>()
        {
            FlightFactory.Create(),
            FlightFactory.Create(2, "AB1212",dateTime),
            FlightFactory.Create(3, "AB1213")
        };
        var flightDtoList = flightList.Select(f => f.ToDto()).ToList();
        mockFlightService.Setup(s => s.GetUpcomingFlightsAsync(dateTime))
            .ReturnsAsync(flightList);
        var mockMapper = new Mock<IMapper>();
        mockMapper.Setup(m => m.Map<List<FlightDto>>(flightList))
            .Returns(flightDtoList);
        var query = new GetUpcomingFlightsQuery(dateTime);
        var handler = new GetUpcomingFlightsHandler(mockFlightService.Object, mockMapper.Object);

        // Act
        var result = await handler.Handle(query, CancellationToken.None);
        var resultList = result.ToList();

        // Assert
        for (int i = 0; i < flightList.Count; i++)
            resultList[i].Should().BeEquivalentTo(flightDtoList[i]);
    }
}