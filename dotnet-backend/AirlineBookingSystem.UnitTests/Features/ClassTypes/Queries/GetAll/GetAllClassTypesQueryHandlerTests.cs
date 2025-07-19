using AirlineBookingSystem.Application.Features.ClassTypes.Queries.GetAll;
using AirlineBookingSystem.Application.Interfaces.UnitOfWork;
using AirlineBookingSystem.Domain.Entities;
using AirlineBookingSystem.Shared.DTOs.ClassTypes;
using AirlineBookingSystem.UnitTests.Common.TestData;
using AutoMapper;
using FluentAssertions;
using Moq;

namespace AirlineBookingSystem.UnitTests.Features.ClassTypes.Queries.GetAll;

public class GetAllClassTypesQueryHandlerTests
{
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly GetAllClassTypesQueryHandler _handler;

    public GetAllClassTypesQueryHandlerTests()
    {
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _mapperMock = new Mock<IMapper>();
        _handler = new GetAllClassTypesQueryHandler(_unitOfWorkMock.Object, _mapperMock.Object);
    }

    [Fact]
    public async Task Handle_Should_ReturnAllClassTypes()
    {
        // Arrange
        var classTypes = new List<ClassType>
        {
            ClassTypeFactory.GetClassTypeFaker().Generate(),
            ClassTypeFactory.GetClassTypeFaker().Generate()
        };
        var classTypeDtos = new List<ClassTypeDto>
        {
            new ClassTypeDto { Id = classTypes[0].Id, Name = classTypes[0].Name.ToString() },
            new ClassTypeDto { Id = classTypes[1].Id, Name = classTypes[1].Name.ToString() }
        };

        _unitOfWorkMock.Setup(u => u.ClassTypes.GetAllAsync()).ReturnsAsync(classTypes);
        _mapperMock.Setup(m => m.Map<IEnumerable<ClassTypeDto>>(classTypes)).Returns(classTypeDtos);

        // Act
        var result = await _handler.Handle(new GetAllClassTypesQuery(), CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().BeEquivalentTo(classTypeDtos);
    }

    [Fact]
    public async Task Handle_Should_ReturnEmptyList_WhenNoClassTypesExist()
    {
        // Arrange
        var classTypes = new List<ClassType>();
        var classTypeDtos = new List<ClassTypeDto>();
        if (classTypeDtos == null) throw new ArgumentNullException(nameof(classTypeDtos));

        _unitOfWorkMock.Setup(u => u.ClassTypes.GetAllAsync()).ReturnsAsync(classTypes);
        _mapperMock.Setup(m => m.Map<IEnumerable<ClassTypeDto>>(classTypes)).Returns(classTypeDtos);

        // Act
        var result = await _handler.Handle(new GetAllClassTypesQuery(), CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().BeEmpty();
    }
}
