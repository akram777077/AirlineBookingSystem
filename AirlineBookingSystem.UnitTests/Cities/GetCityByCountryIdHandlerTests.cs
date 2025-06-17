using AirlineBookingSystem.Application.Features.Cities.Queries.ByCountryId;
using AirlineBookingSystem.Application.Interfaces.Repositories;
using AirlineBookingSystem.Domain.Entities;
using AirlineBookingSystem.Shared.DTOs.Countries;
using AutoMapper;
using Moq;

namespace AirlineBookingSystem.UnitTests.Cities;

public class GetCityByCountryIdHandlerTests
{
    [Fact]
    public async Task Handle_ShouldReturnCities_WhenCountryExists()
    {
        // Arrange
        var countryId = 1;

        var cityEntities = new List<City>
        {
            new() { Id = 1, Name = "Oran", CountryId = countryId, Country = new Country { Id = 1, Name = "Algeria", Code = "DZ" } },
            new() { Id = 2, Name = "Algiers", CountryId = countryId, Country = new Country { Id = 1, Name = "Algeria", Code = "DZ" } }
        };

        var cityDtos = new List<CityDto>
        {
            new() { Id = 1, Name = "Oran", CountryId = countryId },
            new() { Id = 2, Name = "Algiers", CountryId = countryId }
        };

        var cityRepositoryMock = new Mock<ICityRepository>();
        cityRepositoryMock
            .Setup(repo => repo.GetByCountryIdAsync(countryId))
            .ReturnsAsync(cityEntities);

        var mapperMock = new Mock<IMapper>();
        mapperMock
            .Setup(m => m.Map<IReadOnlyCollection<CityDto>>(cityEntities)) 
            .Returns(cityDtos);

        var handler = new GetCityByCountryIdHandler(cityRepositoryMock.Object, mapperMock.Object);
        var query = new GetCityByCountryIdQuery(countryId);

        // Act
        var result = await handler.Handle(query, CancellationToken.None);
        var resultList = result.ToList();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, resultList.Count);
        Assert.Equal("Oran", resultList[0].Name);
        Assert.Equal("Algiers", resultList[1].Name);
        cityRepositoryMock.Verify(repo => repo.GetByCountryIdAsync(countryId), Times.Once);
    }
}
