using AirlineBookingSystem.Application.Features.Countries.Queries.GetById;
using AirlineBookingSystem.Application.Interfaces.UnitOfWork;
using AirlineBookingSystem.Domain.Entities;
using AirlineBookingSystem.Shared.DTOs.countries;
using AutoMapper;
using FluentAssertions;
using Moq;

namespace AirlineBookingSystem.UnitTests.Features.Countries.Queries.GetById;

public class GetCountryByIdQueryHandlerTests
{
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly GetCountryByIdQueryHandler _handler;

    public GetCountryByIdQueryHandlerTests()
    {
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _mapperMock = new Mock<IMapper>();
        _handler = new GetCountryByIdQueryHandler(_unitOfWorkMock.Object, _mapperMock.Object);
    }

    [Fact]
    public async Task Handle_Should_ReturnCountry_WhenCountryExists()
    {
        // Arrange
        var countryId = 1;
        var country = new Country { Id = countryId, Name = "Test Country", Code = "TC" };
        var countryDto = new CountryDto { Id = countryId, Name = "Test Country", Code = "TC" };

        _unitOfWorkMock.Setup(u => u.Countries.GetByIdAsync(countryId)).ReturnsAsync(country);
        _mapperMock.Setup(m => m.Map<CountryDto>(country)).Returns(countryDto);

        // Act
        var result = await _handler.Handle(new GetCountryByIdQuery(countryId), CancellationToken.None);

        // Assert
        result.Should().BeEquivalentTo(countryDto);
    }
}
