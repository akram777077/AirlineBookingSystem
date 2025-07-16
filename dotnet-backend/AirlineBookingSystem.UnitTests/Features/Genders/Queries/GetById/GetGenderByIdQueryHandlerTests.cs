using AirlineBookingSystem.Application.Features.Genders.Queries.GetById;
using AirlineBookingSystem.Application.Interfaces.UnitOfWork;
using AirlineBookingSystem.Domain.Entities;
using AirlineBookingSystem.Shared.DTOs.Genders;
using AirlineBookingSystem.Shared.Results;
using AirlineBookingSystem.UnitTests.Common.TestData;
using AutoMapper;
using FluentAssertions;
using Moq;
using Xunit;

namespace AirlineBookingSystem.UnitTests.Features.Genders.Queries.GetById;

public class GetGenderByIdQueryHandlerTests
{
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly GetGenderByIdQueryHandler _handler;

    public GetGenderByIdQueryHandlerTests()
    {
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _mapperMock = new Mock<IMapper>();
        _handler = new GetGenderByIdQueryHandler(_unitOfWorkMock.Object, _mapperMock.Object);
    }

    [Fact]
    public async Task Handle_ShouldReturnGenderDto_WhenGenderExists()
    {
        // Arrange
        var genderId = 1;
        var query = new GetGenderByIdQuery(genderId);
        var gender = GenderFactory.GetGenderFaker().Generate();
        gender.Id = genderId;
        var genderDto = new GenderDto { Id = genderId, Code = gender.Code.ToString() };

        _unitOfWorkMock.Setup(u => u.Genders.GetByIdAsync(genderId))
            .ReturnsAsync(gender);
        _mapperMock.Setup(m => m.Map<GenderDto>(gender))
            .Returns(genderDto);

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().Be(genderDto);
        result.StatusCode.Should().Be(ResultStatusCode.Success);
    }

    [Fact]
    public async Task Handle_ShouldReturnNotFound_WhenGenderDoesNotExist()
    {
        // Arrange
        var genderId = 1;
        var query = new GetGenderByIdQuery(genderId);

        _unitOfWorkMock.Setup(u => u.Genders.GetByIdAsync(genderId))
            .ReturnsAsync((Gender)null);

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.IsSuccess.Should().BeFalse();
        result.StatusCode.Should().Be(ResultStatusCode.NotFound);
        result.Error.Should().Be("Gender not found.");
    }
}
