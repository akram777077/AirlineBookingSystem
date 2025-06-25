using AirlineBookingSystem.Application.Features.Countries.Queries.ById;
using AirlineBookingSystem.Application.Interfaces.Repositories;
using AirlineBookingSystem.Domain.Entities;
using AirlineBookingSystem.Shared.DTOs.Countries;
using AutoMapper;
using Moq;

namespace AirlineBookingSystem.UnitTests.Features.Countries.Queries;

public class GetCountryByIdHandlerTests
{
    [Fact]
    public async Task Handle_ExistingId_ReturnsCountryDto()
    {
        var mockRepo = new Mock<ICountryRepository>();
        var mockMapper = new Mock<IMapper>();

        var country = new Country { Id = 1, Name = "Algeria", Code = "DZ" };
        var dto = new CountryDto {Id = 1, Name = "Algeria", Code = "DZ" };

        mockRepo.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(country);
        mockMapper.Setup(m => m.Map<CountryDto>(country)).Returns(dto);

        var handler = new GetCountryByIdQueryHandler(mockRepo.Object, mockMapper.Object);
        var result = await handler.Handle(new GetCountryByIdQuery(1), CancellationToken.None);

        Assert.NotNull(result);
        Assert.Equal("Algeria", result.Name);
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
        Assert.Null(result);
    }

}
