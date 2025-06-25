using AirlineBookingSystem.Application.Features.Countries.Queries.All;
using AirlineBookingSystem.Application.Interfaces.Repositories;
using AirlineBookingSystem.Domain.Entities;
using AirlineBookingSystem.Shared.DTOs.Countries;
using AutoMapper;
using Moq;

namespace AirlineBookingSystem.UnitTests.Features.Countries.Queries;

public class GetAllCountriesHandlerTests
{
    [Fact]
    public async Task Handle_ReturnsAllCountries()
    {
        // Arrange
        var mockRepo = new Mock<ICountryRepository>();
        var mockMapper = new Mock<IMapper>();

        var countryEntities = new List<Country>
        {
            new() { Id = 1, Name = "Algeria", Code = "DZ" },
            new() { Id = 2, Name = "Tunisia", Code = "TN" }
        };

        var countryDtos = new List<CountryDto>
        {
            new() {Id = 1 ,Name = "Algeria", Code = "DZ" },
            new() {Id = 2, Name = "Tunisia", Code = "TN" }
        };

        mockRepo.Setup(repo => repo.GetAllAsync()).ReturnsAsync(countryEntities);
        mockMapper.Setup(m => m.Map<List<CountryDto>>(countryEntities)).Returns(countryDtos);

        var handler = new GetAllCountriesQueryHandler(mockRepo.Object, mockMapper.Object);

        // Act
        var result = await handler.Handle(new GetAllCountriesQuery(), CancellationToken.None);

        // Assert
        var resultList = result.ToList();
        Assert.NotNull(resultList);
        Assert.Equal(2, resultList.Count);
        Assert.Equal("Algeria", resultList[0].Name);
        Assert.Equal("Tunisia", resultList[1].Name);
    }
}