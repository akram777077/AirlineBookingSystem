using AirlineBookingSystem.Application.Features.BookingStatuses.Queries.ById;
using AirlineBookingSystem.Application.Interfaces.Repositories;
using AirlineBookingSystem.Domain.Entities;
using AirlineBookingSystem.Shared.DTOs.BookingStatus;
using AirlineBookingSystem.Shared.Enums;
using AutoMapper;
using Moq;

namespace AirlineBookingSystem.UnitTests.BookingStatuses;

public class GetBookingStatusesByIdHandlerTests
{
    [Fact]
    public async Task GetBookingStatusesByIdHandler_ShouldReturnBookingStatus_WhenIdExists()
    {
        //Arrange
        var bookingStatus = new BookingStatus
        {
            Id = 1,
            StatusName = BookingStatusEnum.CheckedIn
        };
        var bookingStatusDto = new BookingStatusDto
        {
            Id = 1,
            StatusName = "CheckedIn"
        };
        var bookingStatusRepository = new Mock<IBookingStatusRepository>();
        bookingStatusRepository
            .Setup(repo => repo.GetByIdAsync(1))
            .ReturnsAsync(bookingStatus);
        var mapper = new Mock<IMapper>();
        mapper
            .Setup(m => m.Map<BookingStatusDto>(bookingStatus))
            .Returns((BookingStatus status) => bookingStatusDto);
        var handler = new GetBookingStatusesByIdHandler(bookingStatusRepository.Object,mapper.Object);
        var query = new GetBookingStatusesByIdQuery(1);
        //Act
        var result = await handler.Handle(query, CancellationToken.None);
        //Assert
        Assert.NotNull(result);
        Assert.Equal(bookingStatusDto.Id, result.Id);
        Assert.Equal(bookingStatusDto.StatusName, result.StatusName);
    }
    [Fact]
    public async Task Handle_ReturnsNull_WhenBookingStatusNotFound()
    {
        // Arrange
        var repositoryMock = new Mock<IBookingStatusRepository>();
        repositoryMock
            .Setup(repo => repo.GetByIdAsync(It.IsAny<int>()))
            .ReturnsAsync((BookingStatus?)null);

        var mapperMock = new Mock<IMapper>();

        var handler = new GetBookingStatusesByIdHandler(repositoryMock.Object, mapperMock.Object);

        var query = new GetBookingStatusesByIdQuery(-1);

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.Null(result);
    }
}