using AirlineBookingSystem.Application.Features.Airports.Queries.ById;
using AirlineBookingSystem.Application.Interfaces.Repositories;
using AirlineBookingSystem.Domain.Entities;
using AirlineBookingSystem.Shared.DTOs.Airports;
using AirlineBookingSystem.UnitTests.Common.TestData;
using AutoMapper;
using FluentAssertions;
using Moq;

namespace AirlineBookingSystem.UnitTests.Features.Airports.Queries;

public class GetAirportByIdHandlerTests
{
    [Fact]
    public async Task GetAirportById_ShouldReturnAirport_WhenExists()
    {
        var mockAirportRepository = new Mock<IAirportRepository>();
        var mockMapper = new Mock<IMapper>();
        var airportId = 1;
        var airportEntity = AirportFactory.Create(airportId, "Oran Airport", "ORN");
        var airportDto = airportEntity.ToDto();
        
        mockAirportRepository
            .Setup(repo => repo.GetByIdAsync(airportId))
            .ReturnsAsync(airportEntity);
        mockMapper
            .Setup(m => m.Map<AirportDto>(It.IsAny<Airport>()))
            .Returns(airportDto);
        var handler = new GetAirportByIdHandler(mockAirportRepository.Object, mockMapper.Object);
        var query = new GetAirportByIdQuery(airportId);
        // Act
        var result = await handler.Handle(query, CancellationToken.None);
        // Assert
        result.Should().BeEquivalentTo(airportDto);
    }
    [Fact]
    public async Task GetAirportById_ShouldReturnNull_WhenDoesNotExist()
    {
        var mockAirportRepository = new Mock<IAirportRepository>();
        var mockMapper = new Mock<IMapper>();
        var airportId = -1;
        mockAirportRepository
            .Setup(repo => repo.GetByIdAsync(airportId))
            .ReturnsAsync((Airport?)null);
        var handler = new GetAirportByIdHandler(mockAirportRepository.Object, mockMapper.Object);
        var query = new GetAirportByIdQuery(airportId);
        // Act
        var result = await handler.Handle(query, CancellationToken.None);
        // Assert
        Assert.Null(result);
        mockAirportRepository.Verify(repo => repo.GetByIdAsync(airportId), Times.Once);
    }
    
}