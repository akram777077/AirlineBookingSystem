using AirlineBookingSystem.Application.Features.Cities.Queries.ById;
using AirlineBookingSystem.Application.Interfaces.Repositories;
using AirlineBookingSystem.Domain.Entities;
using AirlineBookingSystem.Shared.DTOs.Cities;
using AirlineBookingSystem.UnitTests.Common.TestData;
using AutoMapper;
using FluentAssertions;
using Moq;

namespace AirlineBookingSystem.UnitTests.Features.Cities.Queries;

public class GetCityByIdHandlerTests
{
    [Fact]
    public async Task GetCityById_ShouldReturnCity_WhenCityExists()
    {
        // Arrange
        var cityEntities = new List<City>
        { 
            CityFactory.Create()
        };

        var cityDtos = new List<CityDto>
        {
            new () { Id = cityEntities[0].Id, Name = cityEntities[0].Name, CountryId = cityEntities[0].CountryId }
        };
        var cityRepositoryMock = new Mock<ICityRepository>();
        cityRepositoryMock
            .Setup(repo => repo.GetByIdAsync(1))
            .ReturnsAsync(cityEntities.FirstOrDefault(c => c.Id == 1));
        var mapperMock = new Mock<IMapper>();
        mapperMock
            .Setup(m => m.Map<CityDto>(It.IsAny<City>()))
            .Returns((City city) => cityDtos.FirstOrDefault(c => c.Id == city.Id)!);
        var handler = new GetCityByIdHandler(cityRepositoryMock.Object, mapperMock.Object);
        var query = new GetCityByIdQuery(1);
        // Act
        var result = await handler.Handle(query, CancellationToken.None);
        // Assert
        result.Should().NotBeNull()
            .And
            .BeEquivalentTo(cityDtos[0]);
    }
    [Fact]
    public async Task GetCityById_ShouldReturnNull_WhenCityDoesNotExist()
    {
        // Arrange
        var cityRepositoryMock = new Mock<ICityRepository>();
        cityRepositoryMock
            .Setup(repo => repo.GetByIdAsync(2))
            .ReturnsAsync((City?)null);
        var mapperMock = new Mock<IMapper>();
        var handler = new GetCityByIdHandler(cityRepositoryMock.Object, mapperMock.Object);
        var query = new GetCityByIdQuery(-1);
        // Act
        var result = await handler.Handle(query, CancellationToken.None);
        // Assert
        result.Should().BeNull();
    }
}