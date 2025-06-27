using AirlineBookingSystem.Application.Features.Airports.Queries.ByCountryIdAndCityId;
using AirlineBookingSystem.Application.Interfaces.Repositories;
using AirlineBookingSystem.Domain.Entities;
using AirlineBookingSystem.Shared.DTOs.Airports;
using AutoMapper;
using Moq;

namespace AirlineBookingSystem.UnitTests.Features.Airports.Queries;

public class GetAirportsByCountryIdAndCityIdHandlerTests
{
    [Fact]
    public async Task Handle_ShouldReturnAirports_WhenCountryIdAndCityIdAreValid()
    {
        // Arrange
        var countryId = 1;
        var cityId = 1;
        var mockAirportRepository = new Mock<IAirportRepository>();
        var mockMapper = new Mock<IMapper>();
        var airportId = 1;
        var airportsEntity = new List<Airport>
        {
            new()
            {
                Id = airportId,
                Name = "Oran Airport",
                AirportCode = "ORN",
                CityId = 1,
                City = new City
                {
                    Id = 1, Name = "Oran", CountryId = 1,
                    Country = new Country { Id = 1, Name = "Algeria", Code = "DZ" }
                }
            },
            new()
            {
                Id = 2,
                Name = "Algiers Airport",
                AirportCode = "ALG",
                CityId = 1,
                City = new City
                {
                    Id = 1, Name = "Algiers", CountryId = 1,
                    Country = new Country { Id = 1, Name = "Algeria", Code = "DZ" }
                }
            }
        };
        var airportsDto = new List<AirportDto>
        {
            new()
            {
                Id = airportId,
                Name = "Oran Airport",
                AirportCode = "ORN",
                CityId = 1
            },
            new()
            {
                Id = 2,
                Name = "Algiers Airport",
                AirportCode = "ALG",
                CityId = 1
            }
        };
        mockAirportRepository
            .Setup(repo => repo.GetByCountryIdAndCityIdAsync(countryId, cityId))
            .ReturnsAsync(airportsEntity);
        mockMapper
            .Setup(m => m.Map<IReadOnlyCollection<AirportDto>>(airportsEntity))
            .Returns(airportsDto);
        var handler = new GetAirportsByCountryIdAndCityIdHandler(mockAirportRepository.Object, mockMapper.Object);
        var query = new GetAirportsByCountryIdAndCityIdQuery(countryId, cityId);
        // Act
        var result = await handler.Handle(query, CancellationToken.None);
        var resultList = result.ToList();
        // Assert
        Assert.NotNull(resultList);
        Assert.Equal(2, resultList.Count);
        Assert.Equal("Oran Airport", resultList[0].Name);
        Assert.Equal("ORN", resultList[0].AirportCode);
        Assert.Equal(1, resultList[0].CityId);
        Assert.Equal("Algiers Airport", resultList[1].Name);
        Assert.Equal("ALG", resultList[1].AirportCode);
        Assert.Equal(1, resultList[1].CityId);
        mockAirportRepository.Verify(repo => repo.GetByCountryIdAndCityIdAsync(countryId, cityId), Times.Once);


    }
    [Fact]
    public async Task Handle_ShouldReturnEmptyList_WhenNoAirportsFound()
    {
        // Arrange
        var countryId = 1;
        var cityId = 1;
        var mockAirportRepository = new Mock<IAirportRepository>();
        var mockMapper = new Mock<IMapper>();
        mockAirportRepository
            .Setup(repo => repo.GetByCountryIdAndCityIdAsync(countryId, cityId))
            .ReturnsAsync(new List<Airport>());
        mockMapper
            .Setup(m => m.Map<IReadOnlyCollection<AirportDto>>(It.IsAny<List<Airport>>()))
            .Returns(new List<AirportDto>());
        var handler = new GetAirportsByCountryIdAndCityIdHandler(mockAirportRepository.Object, mockMapper.Object);
        var query = new GetAirportsByCountryIdAndCityIdQuery(countryId, cityId);
        // Act
        var result = await handler.Handle(query, CancellationToken.None);
        // Assert
        Assert.Empty(result);
        mockAirportRepository.Verify(repo => repo.GetByCountryIdAndCityIdAsync(countryId, cityId), Times.Once);
    }
}