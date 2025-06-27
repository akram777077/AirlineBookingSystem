using AirlineBookingSystem.Application.Features.Countries.Queries.ById;
using AirlineBookingSystem.Application.Interfaces.Repositories;
using AirlineBookingSystem.Domain.Entities;
using AirlineBookingSystem.Shared.DTOs.Countries;
using AirlineBookingSystem.UnitTests.Common.TestData;
using AutoMapper;
using FluentAssertions;
using Moq;

namespace AirlineBookingSystem.UnitTests.Features.Countries.Queries;

public class GetCountryByIdHandlerTests
{
    [Fact]
    public async Task Handle_ExistingId_ReturnsCountryDto()
    {
        var mockRepo = new Mock<ICountryRepository>();
        var mockMapper = new Mock<IMapper>();

        var country = CountryFactory.Create();
        var dto = new CountryDto {Id = country.Id, Name = country.Name, Code = country.Code};

        mockRepo.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(country);
        mockMapper.Setup(m => m.Map<CountryDto>(country)).Returns(dto);

        var handler = new GetCountryByIdQueryHandler(mockRepo.Object, mockMapper.Object);
        var result = await handler.Handle(new GetCountryByIdQuery(1), CancellationToken.None);

        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(dto);
    }
    [Fact]
    public async Task Handle_NonExistingId_ReturnsNull()
    {
        // Arrange
        var mockRepo = new Mock<ICountryRepository>();
        var mockMapper = new Mock<IMapper>();

        mockRepo.Setup(r => r.GetByIdAsync(It.IsAny<int>())).ReturnsAsync((Country?)null);

        var handler = new GetCountryByIdQueryHandler(mockRepo.Object, mockMapper.Object);
        var query = new GetCountryByIdQuery(-1); 

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        result.Should().BeNull();
    }

}
