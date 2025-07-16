using AirlineBookingSystem.Application.Features.Cities.Queries.GetById;
using AirlineBookingSystem.Application.Interfaces.UnitOfWork;
using AirlineBookingSystem.Domain.Entities;
using AirlineBookingSystem.Shared.DTOs.Cities;
using AutoMapper;
using FluentAssertions;
using Moq;

namespace AirlineBookingSystem.UnitTests.Features.Cities.Queries.GetById;

public class GetCityByIdQueryHandlerTests
{
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly GetCityByIdQueryHandler _handler;

    public GetCityByIdQueryHandlerTests()
    {
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _mapperMock = new Mock<IMapper>();
        _handler = new GetCityByIdQueryHandler(_unitOfWorkMock.Object, _mapperMock.Object);
    }

    [Fact]
    public async Task Handle_Should_ReturnCity_WhenCityExists()
    {
        // Arrange
        var cityId = 1;
        var city = new City { Id = cityId, Name = "Test City", CountryId = 1, Country = new Country { Id = 1, Name = "Test Country", Code = "TC" } };
        var cityDto = new CityDto { Id = cityId, Name = "Test City", CountryId = 1 };

        _unitOfWorkMock.Setup(u => u.Cities.GetByIdAsync(cityId)).ReturnsAsync(city);
        _mapperMock.Setup(m => m.Map<CityDto>(city)).Returns(cityDto);

        // Act
        var result = await _handler.Handle(new GetCityByIdQuery(cityId), CancellationToken.None);

        // Assert
        result.Should().BeEquivalentTo(cityDto);
    }
}
