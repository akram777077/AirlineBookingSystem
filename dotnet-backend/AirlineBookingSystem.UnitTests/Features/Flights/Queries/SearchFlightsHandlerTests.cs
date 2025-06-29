using AirlineBookingSystem.Application.Features.Flights.Queries.Search;
using AirlineBookingSystem.Application.Interfaces.Services;
using AirlineBookingSystem.Domain.Entities;
using AirlineBookingSystem.Shared.DTOs.Flights;
using AirlineBookingSystem.UnitTests.Common.TestData;
using AutoMapper;
using FluentAssertions;
using Moq;
using Xunit;

namespace AirlineBookingSystem.UnitTests.Features.Flights.Queries;

public class SearchFlightsHandlerTests
{
    [Fact]
    public async Task SearchFlightsHandler_ShouldReturnFlights_WhenFlightsExist()
    {
        // Arrange
        var fromCode = "JFK";
        var toCode = "LAX";
        var date = DateTime.UtcNow.Date;

        var expectedFlights = new List<Flight>
        {
            FlightFactory.Create(1),
            FlightFactory.Create(2)
        };

        expectedFlights[0].FromAirport.AirportCode = fromCode;
        expectedFlights[0].ToAirport.AirportCode = toCode;
        expectedFlights[0].DepartureTime = date;

        var expectedDtos = expectedFlights
            .Where(f => f.Id == 1) // match only one that satisfies the condition
            .Select(f => f.ToDto())
            .ToList();

        var mockFlightService = new Mock<IFlightService>();
        mockFlightService
            .Setup(s => s.SearchFlightsAsync(fromCode, toCode, date))
            .ReturnsAsync(expectedFlights.Where(f => f.Id == 1).ToList());

        var mockMapper = new Mock<IMapper>();
        mockMapper
            .Setup(m => m.Map<List<FlightDto>>(It.IsAny<List<Flight>>()))
            .Returns(expectedDtos);

        var query = new SearchFlightsQuery(fromCode, toCode, date);
        var handler = new SearchFlightsHandler(mockFlightService.Object, mockMapper.Object);

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(expectedDtos);
    }
}