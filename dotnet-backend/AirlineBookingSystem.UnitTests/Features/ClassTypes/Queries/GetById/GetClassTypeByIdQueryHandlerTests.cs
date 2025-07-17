using AirlineBookingSystem.Application.Features.ClassTypes.Queries.GetById;
using AirlineBookingSystem.Application.Interfaces.UnitOfWork;
using AirlineBookingSystem.Domain.Entities;
using AirlineBookingSystem.Shared.DTOs.ClassTypes;
using AirlineBookingSystem.Shared.Results;
using AirlineBookingSystem.UnitTests.Common.TestData;
using AutoMapper;
using FluentAssertions;
using Moq;
using Xunit;

namespace AirlineBookingSystem.UnitTests.Features.ClassTypes.Queries.GetById;

public class GetClassTypeByIdQueryHandlerTests
{
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly GetClassTypeByIdQueryHandler _handler;

    public GetClassTypeByIdQueryHandlerTests()
    {
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _mapperMock = new Mock<IMapper>();
        _handler = new GetClassTypeByIdQueryHandler(_unitOfWorkMock.Object, _mapperMock.Object);
    }

    [Fact]
    public async Task Handle_ShouldReturnClassTypeDto_WhenClassTypeExists()
    {
        // Arrange
        var classTypeId = 1;
        var query = new GetClassTypeByIdQuery(classTypeId);
        var classType = ClassTypeFactory.GetClassTypeFaker().Generate();
        classType.Id = classTypeId;
        var classTypeDto = new ClassTypeDto { Id = classTypeId, Name = classType.Name.ToString() };

        _unitOfWorkMock.Setup(u => u.ClassTypes.GetByIdAsync(classTypeId))
            .ReturnsAsync(classType);
        _mapperMock.Setup(m => m.Map<ClassTypeDto>(classType))
            .Returns(classTypeDto);

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().Be(classTypeDto);
        result.StatusCode.Should().Be(ResultStatusCode.Success);
    }

    [Fact]
    public async Task Handle_ShouldReturnNotFound_WhenClassTypeDoesNotExist()
    {
        // Arrange
        var classTypeId = 1;
        var query = new GetClassTypeByIdQuery(classTypeId);

        _unitOfWorkMock.Setup(u => u.ClassTypes.GetByIdAsync(classTypeId))
            .ReturnsAsync((ClassType?)null);

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.IsSuccess.Should().BeFalse();
        result.StatusCode.Should().Be(ResultStatusCode.NotFound);
        result.Error.Should().Be("Class type not found.");
    }
}
