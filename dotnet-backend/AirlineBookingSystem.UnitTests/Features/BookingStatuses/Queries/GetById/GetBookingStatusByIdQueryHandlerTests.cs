using AirlineBookingSystem.Application.Features.BookingStatuses.Queries.GetById;
using AirlineBookingSystem.Application.Interfaces.UnitOfWork;
using AirlineBookingSystem.Domain.Entities;
using AirlineBookingSystem.Shared.DTOs.BookingStatuses;
using AirlineBookingSystem.Shared.Results;
using AirlineBookingSystem.UnitTests.Common.TestData;
using AutoMapper;
using FluentAssertions;
using Moq;
using Xunit;

namespace AirlineBookingSystem.UnitTests.Features.BookingStatuses.Queries.GetById;

public class GetBookingStatusByIdQueryHandlerTests
{
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly GetBookingStatusByIdQueryHandler _handler;

    public GetBookingStatusByIdQueryHandlerTests()
    {
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _mapperMock = new Mock<IMapper>();
        _handler = new GetBookingStatusByIdQueryHandler(_unitOfWorkMock.Object, _mapperMock.Object);
    }

    [Fact]
    public async Task Handle_ShouldReturnBookingStatusDto_WhenBookingStatusExists()
    {
        // Arrange
        var bookingStatusId = 1;
        var query = new GetBookingStatusByIdQuery(bookingStatusId);
        var bookingStatus = BookingStatusFactory.GetBookingStatusFaker().Generate();
        bookingStatus.Id = bookingStatusId;
        var bookingStatusDto = new BookingStatusDto { Id = bookingStatusId, Name = bookingStatus.BookingStatusName.ToString() };

        _unitOfWorkMock.Setup(u => u.BookingStatuses.GetByIdAsync(bookingStatusId))
            .ReturnsAsync(bookingStatus);
        _mapperMock.Setup(m => m.Map<BookingStatusDto>(bookingStatus))
            .Returns(bookingStatusDto);

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().Be(bookingStatusDto);
        result.StatusCode.Should().Be(ResultStatusCode.Success);
    }

    [Fact]
    public async Task Handle_ShouldReturnNotFound_WhenBookingStatusDoesNotExist()
    {
        // Arrange
        var bookingStatusId = 1;
        var query = new GetBookingStatusByIdQuery(bookingStatusId);

        _unitOfWorkMock.Setup(u => u.BookingStatuses.GetByIdAsync(bookingStatusId))
            .ReturnsAsync((BookingStatus?)null);

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.IsSuccess.Should().BeFalse();
        result.StatusCode.Should().Be(ResultStatusCode.NotFound);
        result.Error.Should().Be("Booking status not found.");
    }
}
