using AirlineBookingSystem.Application.Features.Cities.Queries.ByCountryId;
using AirlineBookingSystem.Application.Interfaces.Repositories;
using AirlineBookingSystem.Domain.Entities;
using AirlineBookingSystem.Shared.DTOs.Cities;
using AirlineBookingSystem.UnitTests.Common.TestData;
using AutoMapper;
using FluentAssertions;
using Moq;

namespace AirlineBookingSystem.UnitTests.Features.Cities.Queries;

public class GetCityByCountryIdHandlerTests
{
    [Fact]
    public async Task Handle_ShouldReturnCities_WhenCountryExists()
    {
        // Arrange
        var countryId = 1;

        var cityEntities = new List<City>
        {
            CityFactory.Create(),
            CityFactory.Create(2,"Oran")
        };

        var cityDtos = new List<CityDto>
        {
            new() {Id = cityEntities[0].Id, Name = cityEntities[0].Name, CountryId = cityEntities[0].CountryId},
            new() {Id = cityEntities[1].Id, Name = cityEntities[1].Name, CountryId = cityEntities[1].CountryId}
        };

        var cityRepositoryMock = new Mock<ICityRepository>();
        cityRepositoryMock
            .Setup(repo => repo.GetByCountryIdAsync(countryId))
            .ReturnsAsync(cityEntities);

        var mapperMock = new Mock<IMapper>();
        mapperMock
            .Setup(m => m.Map<IReadOnlyCollection<CityDto>>(cityEntities)) 
            .Returns(cityDtos);

        var handler = new GetCitiesByCountryIdHandler(cityRepositoryMock.Object, mapperMock.Object);
        var query = new GetCitiesByCountryIdQuery(countryId);

        // Act
        var result = await handler.Handle(query, CancellationToken.None);
        var resultList = result.ToList();

        // Assert
        for (int i = 0; i < resultList.Count; i++)
            resultList[i].Should().BeEquivalentTo(cityDtos[i]);
    }
}
