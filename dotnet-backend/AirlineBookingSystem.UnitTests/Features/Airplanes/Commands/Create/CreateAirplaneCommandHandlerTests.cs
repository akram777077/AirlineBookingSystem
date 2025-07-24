using AirlineBookingSystem.Application.Features.Airplanes.Commands.Create;
using AirlineBookingSystem.Application.Interfaces.Repositories;
using AirlineBookingSystem.Application.Interfaces.UnitOfWork;
using AirlineBookingSystem.Domain.Entities;
using AirlineBookingSystem.Shared.DTOs.airplanes;
using AirlineBookingSystem.Shared.Results;
using AirlineBookingSystem.UnitTests.Common.TestData;
using AutoMapper;
using FluentAssertions;
using FluentValidation;
using FluentValidation.Results;
using Moq;

namespace AirlineBookingSystem.UnitTests.Features.Airplanes.Commands.Create;

public class CreateAirplaneCommandHandlerTests
{
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly Mock<IValidator<CreateAirplaneCommand>> _validatorMock;
    private readonly CreateAirplaneCommandHandler _handler;
    private readonly Mock<IAirplaneRepository> _airplaneRepositoryMock;

    public CreateAirplaneCommandHandlerTests()
    {
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _mapperMock = new Mock<IMapper>();
        _validatorMock = new Mock<IValidator<CreateAirplaneCommand>>();
        _airplaneRepositoryMock = new Mock<IAirplaneRepository>();
        _unitOfWorkMock.Setup(u => u.Airplanes).Returns(_airplaneRepositoryMock.Object);
        _handler = new CreateAirplaneCommandHandler(_unitOfWorkMock.Object, _mapperMock.Object, _validatorMock.Object);
    }

    [Fact]
    public async Task Handle_ShouldReturnSuccess_WhenAirplaneIsCreatedSuccessfully()
    {
        // Arrange
        var createAirplaneDto = new CreateAirplaneDto("Boeing 747", "Boeing", 400, "B747");
        var command = new CreateAirplaneCommand(createAirplaneDto);
        var airplane = AirplaneFactory.GetAirplaneFaker().Generate();
        var airplaneDto = new AirplaneDto(0, "Test Model", "Test Manufacturer", 100, "ABC");

        _validatorMock.Setup(v => v.ValidateAsync(command, CancellationToken.None)).ReturnsAsync(new ValidationResult());
        _mapperMock.Setup(m => m.Map<Airplane>(createAirplaneDto)).Returns(airplane);
        _airplaneRepositoryMock.Setup(r => r.AddAsync(airplane)).Returns(Task.CompletedTask);
        _unitOfWorkMock.Setup(u => u.CompleteAsync()).ReturnsAsync(1);
        _mapperMock.Setup(m => m.Map<AirplaneDto>(airplane)).Returns(airplaneDto);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.IsSuccess.Should().BeTrue();
        result.StatusCode.Should().Be(ResultStatusCode.Created);
        result.Value.Should().Be(airplaneDto);
        _airplaneRepositoryMock.Verify(r => r.AddAsync(airplane), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldReturnConflict_WhenAirplaneCodeAlreadyExists()
    {
        // Arrange
        var createAirplaneDto = new CreateAirplaneDto("Boeing 747", "Boeing", 400, "B747");
        var command = new CreateAirplaneCommand(createAirplaneDto);
        var validationFailure = new ValidationFailure("CreateAirplaneDto.Code", "An airplane with this code already exists.") { ErrorCode = "Conflict" };
        var validationResult = new ValidationResult(new[] { validationFailure });

        _validatorMock.Setup(v => v.ValidateAsync(command, CancellationToken.None)).ReturnsAsync(validationResult);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.IsSuccess.Should().BeFalse();
        result.StatusCode.Should().Be(ResultStatusCode.Conflict);
        result.Error.Should().Be("An airplane with this code already exists.");
        _airplaneRepositoryMock.Verify(r => r.AddAsync(It.IsAny<Airplane>()), Times.Never);
        _unitOfWorkMock.Verify(u => u.CompleteAsync(), Times.Never);
    }
}