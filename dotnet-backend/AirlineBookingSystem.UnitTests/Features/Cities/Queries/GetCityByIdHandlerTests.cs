using AirlineBookingSystem.Application.Features.Cities.Queries.ById;
using AirlineBookingSystem.Application.Interfaces.Repositories;
using AirlineBookingSystem.Domain.Entities;
using AirlineBookingSystem.Shared.DTOs.Cities;
using AutoMapper;
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
            new() { Id = 1, Name = "Oran", CountryId = 1, Country = new Country { Id = 1, Name = "Algeria", Code = "DZ" } }
        };

        var cityDtos = new List<CityDto>
        {
            new() { Id = 1, Name = "Oran", CountryId = 1 }
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
        Assert.NotNull(result);
        Assert.Equal("Oran", result.Name);
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
        Assert.Null(result);
    }
}