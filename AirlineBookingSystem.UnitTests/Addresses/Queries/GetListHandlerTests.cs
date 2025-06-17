using AirlineBookingSystem.Application.Features.Addresses.Queries.List;
using AirlineBookingSystem.Application.Interfaces.Repositories;
using AirlineBookingSystem.Domain.Entities;
using AirlineBookingSystem.Shared.DTOs.Addresses;
using AutoMapper;
using Moq;

namespace AirlineBookingSystem.UnitTests.Addresses.Queries.Helper;

public class GetListHandlerTests
{
    [Fact]
    public async Task Handle_WhenAddressExists_ReturnAddressesList()
    {
        // Arrange
        var addresses = TestData.GetAddressesList();
        var addressDtos = TestData.GetAddressDtoList();

        var addressRepositoryMock = new Mock<IAddressRepository>();
        addressRepositoryMock
            .Setup(repo => repo.GetAllAsync())
            .ReturnsAsync(addresses);

        var mapperMock = new Mock<IMapper>();
        mapperMock.Setup(m => m.Map<IReadOnlyCollection<AddressDto>>(addresses))
            .Returns(addressDtos);

        var handler = new GetListHandler(addressRepositoryMock.Object, mapperMock.Object);
        var query = new GetListQuery();

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.Count);
        Assert.Equal(addressDtos, result);
    }

    [Fact]
    public async Task Handle_WhenAddressListDoesNotExists_ReturnEmptyList()
    {
        // Arrange
        var addressRepositoryMock = new Mock<IAddressRepository>();
        addressRepositoryMock
            .Setup(repo => repo.GetAllAsync())
            .ReturnsAsync(new List<Address>());

        var mapperMock = new Mock<IMapper>();
        mapperMock.Setup(m => m.Map<IReadOnlyCollection<AddressDto>>(It.IsAny<IEnumerable<Address>>()))
            .Returns(new List<AddressDto>());

        var handler = new GetListHandler(addressRepositoryMock.Object, mapperMock.Object);
        var query = new GetListQuery();

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Empty(result);
    }
}