using AirlineBookingSystem.Application.Features.Genders.Queries.GetAll;
using AirlineBookingSystem.Application.Interfaces.UnitOfWork;
using AirlineBookingSystem.Domain.Entities;
using AirlineBookingSystem.Shared.DTOs.Genders;
using AirlineBookingSystem.UnitTests.Common.TestData;
using AutoMapper;
using FluentAssertions;
using Moq;

namespace AirlineBookingSystem.UnitTests.Features.Genders.Queries.GetAll;

public class GetAllGendersQueryHandlerTests
{
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly GetAllGendersQueryHandler _handler;

    public GetAllGendersQueryHandlerTests()
    {
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _mapperMock = new Mock<IMapper>();
        _handler = new GetAllGendersQueryHandler(_unitOfWorkMock.Object, _mapperMock.Object);
    }

    [Fact]
    public async Task Handle_Should_ReturnAllGenders()
    {
        // Arrange
        var genders = new List<Gender>
        {
            GenderFactory.GetGenderFaker().Generate(),
            GenderFactory.GetGenderFaker().Generate()
        };
        var genderDtos = new List<GenderDto>
        {
            new GenderDto { Id = genders[0].Id, Code = genders[0].Code.ToString() },
            new GenderDto { Id = genders[1].Id, Code = genders[1].Code.ToString() }
        };

        _unitOfWorkMock.Setup(u => u.Genders.GetAllAsync()).ReturnsAsync(genders);
        _mapperMock.Setup(m => m.Map<IEnumerable<GenderDto>>(genders)).Returns(genderDtos);

        // Act
        var result = await _handler.Handle(new GetAllGendersQuery(), CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().BeEquivalentTo(genderDtos);
    }

    [Fact]
    public async Task Handle_Should_ReturnEmptyList_WhenNoGendersExist()
    {
        // Arrange
        var genders = new List<Gender>();
        var genderDtos = new List<GenderDto>();

        _unitOfWorkMock.Setup(u => u.Genders.GetAllAsync()).ReturnsAsync(genders);
        _mapperMock.Setup(m => m.Map<IEnumerable<GenderDto>>(genders)).Returns(genderDtos);

        // Act
        var result = await _handler.Handle(new GetAllGendersQuery(), CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().BeEmpty();
    }
}
