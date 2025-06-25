using AirlineBookingSystem.Application.Features.Airports.Queries.ById;
using AirlineBookingSystem.Application.Interfaces.Repositories;
using AirlineBookingSystem.Domain.Entities;
using AirlineBookingSystem.Shared.DTOs.Airports;
using AutoMapper;
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
        var airportEntity = new Airport
        {
            Id = airportId,
            Name = "Oran Airport",
            AirportCode = "ORN",
            CityId = 1,
            City = new City 
                { Id = 1, Name = "Oran", CountryId = 1,
                    Country = new Country { Id = 1, Name = "Algeria", Code = "DZ" } 
                },
            CountryId = 1,
            Country = new Country
            {
                Id = 1, Name = "Algeria", Code = "DZ"
            }
        };
        var airportDto = new AirportDto
        {
            Id = airportId,
            Name = "Oran Airport",
            AirportCode = "ORN",
            CityId = 1,
            CountryId = 1
        };
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
        Assert.NotNull(result);
        Assert.Equal(airportId, result.Id);
        Assert.Equal("Oran Airport", result.Name);
        Assert.Equal("ORN", result.AirportCode);
        Assert.Equal(1, result.CityId);
        Assert.Equal(1, result.CountryId);
        mockAirportRepository.Verify(repo => repo.GetByIdAsync(airportId), Times.Once);
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