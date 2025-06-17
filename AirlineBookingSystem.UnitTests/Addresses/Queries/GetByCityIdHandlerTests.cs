using AirlineBookingSystem.Application.Features.Addresses.Queries.ByCityId;
using AirlineBookingSystem.Application.Features.Addresses.Queries.ById;
using AirlineBookingSystem.Application.Interfaces.Repositories;
using AirlineBookingSystem.Domain.Entities;
using AirlineBookingSystem.Shared.DTOs.Addresses;
using AirlineBookingSystem.UnitTests.Addresses.Queries.Helper;
using AutoMapper;
using Moq;

namespace AirlineBookingSystem.UnitTests.Addresses.Queries;

public class GetByCityIdHandlerTests
{
    [Fact]
    public async Task Handle_WhenAddressExistsByCityId_ReturnAddress()
    {
        // Arrange
        var addresses = TestData.GetAddressesList();
        var address = addresses.FirstOrDefault(x => x.CityId == 2);
        var addressDto = TestData.GetAddressDto();

        var addressRepositoryMock = new Mock<IAddressRepository>();
        addressRepositoryMock
            .Setup(repo => repo.GetByCityIdAsync(2))
            .ReturnsAsync(address);

        var mapperMock = new Mock<IMapper>();
        mapperMock.Setup(m => m.Map<AddressDto>(It.IsAny<Address>()))
            .Returns(addressDto);

        var handler = new GetByCityIdHandler(addressRepositoryMock.Object, mapperMock.Object);
        var query = new GetByCityIdQuery(2);

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.CityId);
    }

    [Fact]
    public async Task Handle_WhenAddressDoesNotExistsByCityId_ReturnNull()
    {
        // Arrange
        var addressRepositoryMock = new Mock<IAddressRepository>();
        addressRepositoryMock
            .Setup(repo => repo.GetByCityIdAsync(-1))
            .ReturnsAsync((Address?)null);

        var mapperMock = new Mock<IMapper>();
        var handler = new GetByCityIdHandler(addressRepositoryMock.Object, mapperMock.Object);
        var query = new GetByCityIdQuery(-1);

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.Null(result);
    }
}