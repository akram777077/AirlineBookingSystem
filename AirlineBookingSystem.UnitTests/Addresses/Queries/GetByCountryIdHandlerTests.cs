using AirlineBookingSystem.Application.Features.Addresses.Queries.ByCountryId;
using AirlineBookingSystem.Application.Interfaces.Repositories;
using AirlineBookingSystem.Domain.Entities;
using AirlineBookingSystem.Shared.DTOs.Addresses;
using AirlineBookingSystem.UnitTests.Addresses.Queries.Helper;
using AutoMapper;
using Moq;

namespace AirlineBookingSystem.UnitTests.Addresses.Queries;

public class GetByCountryIdHandlerTests
{
    [Fact]
    public async Task Handle_WhenAddressExistsByCountryId_ReturnAddress()
    {
        // Arrange
        var addresses = TestData.GetAddressesList();
        var address = addresses.FirstOrDefault(x => x.CountryId == 3);
        var addressDto = TestData.GetAddressDto();

        var addressRepositoryMock = new Mock<IAddressRepository>();
        addressRepositoryMock
            .Setup(repo => repo.GetByCountryIdAsync(3))
            .ReturnsAsync(address);

        var mapperMock = new Mock<IMapper>();
        mapperMock.Setup(m => m.Map<AddressDto>(It.IsAny<Address>()))
            .Returns(addressDto);

        var handler = new GetByCountryIdHandler(addressRepositoryMock.Object, mapperMock.Object);
        var query = new GetByCountryIdQuery(3);

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(3, result.CountryId);
    }

    [Fact]
    public async Task Handle_WhenAddressDoesNotExistsByCountryId_ReturnNull()
    {
        // Arrange
        var addressRepositoryMock = new Mock<IAddressRepository>();
        addressRepositoryMock
            .Setup(repo => repo.GetByCountryIdAsync(-1))
            .ReturnsAsync((Address?)null);

        var mapperMock = new Mock<IMapper>();
        var handler = new GetByCountryIdHandler(addressRepositoryMock.Object, mapperMock.Object);
        var query = new GetByCountryIdQuery(-1);

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.Null(result);
    }
}