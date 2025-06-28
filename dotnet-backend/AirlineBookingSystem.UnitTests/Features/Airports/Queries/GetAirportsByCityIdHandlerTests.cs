using AirlineBookingSystem.Application.Features.Airports.Queries.ByCountryIdAndCityId;
using AirlineBookingSystem.Application.Interfaces.Repositories;
using AirlineBookingSystem.Domain.Entities;
using AirlineBookingSystem.Shared.DTOs.Airports;
using AirlineBookingSystem.UnitTests.Common.TestData;
using AutoMapper;
using FluentAssertions;
using Moq;

namespace AirlineBookingSystem.UnitTests.Features.Airports.Queries;

public class GetAirportsByCityIdHandlerTests
{
    [Fact]
    public async Task Handle_ShouldReturnAirports_WhenCountryIdAndCityIdAreValid()
    {
        // Arrange
        var cityId = 1;
        var mockAirportRepository = new Mock<IAirportRepository>();
        var mockMapper = new Mock<IMapper>();
        var airportsEntity = new List<Airport>
        {
            AirportFactory.Create(cityId),
            AirportFactory.Create(2, "Oran Airport", "ORN")
        };
        var airportsDto = new List<AirportDto>
        {
            new () {Id = airportsEntity[0].Id, Name = airportsEntity[0].Name, AirportCode = airportsEntity[0].AirportCode, CityId = airportsEntity[0].CityId},
            new () {Id = airportsEntity[1].Id, Name = airportsEntity[1].Name, AirportCode = airportsEntity[1].AirportCode, CityId = airportsEntity[1].CityId}
        };
        mockAirportRepository
            .Setup(repo => repo.GetByCityIdAsync(cityId))
            .ReturnsAsync(airportsEntity);
        mockMapper
            .Setup(m => m.Map<IReadOnlyCollection<AirportDto>>(airportsEntity))
            .Returns(airportsDto);
        var handler = new GetAirportsByCityIdHandler(mockAirportRepository.Object, mockMapper.Object);
        var query = new GetAirportsByCityIdQuery(cityId);
        // Act
        var result = await handler.Handle(query, CancellationToken.None);
        var resultList = result.ToList();
        // Assert
        for (int i = 0; i < resultList.Count; i++)
            resultList[i].Should().BeEquivalentTo(airportsDto[i]);



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
            .Setup(repo => repo.GetByCityIdAsync(cityId))
            .ReturnsAsync(new List<Airport>());
        mockMapper
            .Setup(m => m.Map<IReadOnlyCollection<AirportDto>>(It.IsAny<List<Airport>>()))
            .Returns(new List<AirportDto>());
        var handler = new GetAirportsByCityIdHandler(mockAirportRepository.Object, mockMapper.Object);
        var query = new GetAirportsByCityIdQuery(cityId);
        // Act
        var result = await handler.Handle(query, CancellationToken.None);
        // Assert
        result.Should().BeEmpty();
    }
}