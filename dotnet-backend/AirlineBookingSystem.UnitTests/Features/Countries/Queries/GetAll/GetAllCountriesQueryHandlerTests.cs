using AirlineBookingSystem.Application.Features.Countries.Queries.GetAll;
using AirlineBookingSystem.Application.Interfaces.UnitOfWork;
using AirlineBookingSystem.Domain.Entities;
using AirlineBookingSystem.Shared.DTOs.countries;
using AutoMapper;
using FluentAssertions;
using Moq;
using AirlineBookingSystem.Shared.Results;

namespace AirlineBookingSystem.UnitTests.Features.Countries.Queries.GetAll;

public class GetAllCountriesQueryHandlerTests
{
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly GetAllCountriesQueryHandler _handler;

    public GetAllCountriesQueryHandlerTests()
    {
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _mapperMock = new Mock<IMapper>();
        _handler = new GetAllCountriesQueryHandler(_unitOfWorkMock.Object, _mapperMock.Object);
    }

    [Fact]
    public async Task Handle_Should_ReturnAllCountries()
    {
        // Arrange
        var countries = new List<Country> { new() { Id = 1, Name = "Country 1", Code = "C1" }, new() { Id = 2, Name = "Country 2", Code = "C2" } };
        var countryDtos = new List<CountryDto> { new() { Id = countries[0].Id, Name = "Country 1", Code = "C1" }, new() { Id = countries[1].Id, Name = "Country 2", Code = "C2" } };

        _unitOfWorkMock.Setup(u => u.Countries.GetAllAsync()).ReturnsAsync(countries);
        _mapperMock.Setup(m => m.Map<IEnumerable<CountryDto>>(countries)).Returns(countryDtos);

        // Act
        var result = await _handler.Handle(new GetAllCountriesQuery(), CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.IsSuccess.Should().BeTrue();
        result.StatusCode.Should().Be(ResultStatusCode.Success);
        result.Value.Should().BeEquivalentTo(countryDtos);
    }
}
