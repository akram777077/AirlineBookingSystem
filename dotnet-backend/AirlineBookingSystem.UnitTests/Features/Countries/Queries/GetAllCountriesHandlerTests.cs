using AirlineBookingSystem.Application.Features.Countries.Queries.All;
using AirlineBookingSystem.Application.Interfaces.Repositories;
using AirlineBookingSystem.Domain.Entities;
using AirlineBookingSystem.Shared.DTOs.Countries;
using AirlineBookingSystem.UnitTests.Common.TestData;
using AutoMapper;
using FluentAssertions;
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
            CountryFactory.Create(),
            CountryFactory.Create(2, "Tunisia", "TN")
        };

        var countryDtos = new List<CountryDto>
        {
            new CountryDto { Id = countryEntities[0].Id, Name = countryEntities[0].Name, Code = countryEntities[0].Code },
            new CountryDto { Id = countryEntities[1].Id, Name = countryEntities[1].Name, Code = countryEntities[1].Code }
        };

        mockRepo.Setup(repo => repo.GetAllAsync()).ReturnsAsync(countryEntities);
        mockMapper.Setup(m => m.Map<List<CountryDto>>(countryEntities)).Returns(countryDtos);

        var handler = new GetAllCountriesQueryHandler(mockRepo.Object, mockMapper.Object);

        // Act
        var result = await handler.Handle(new GetAllCountriesQuery(), CancellationToken.None);

        // Assert
        var resultList = result.ToList();
        for (int i = 0; i < countryDtos.Count; i++)
        {
            resultList[i].Should().BeEquivalentTo(countryDtos[i]);
        }
    }
}