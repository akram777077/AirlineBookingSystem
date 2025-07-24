using AirlineBookingSystem.Application.Features.Airports.Commands.Create;
using AirlineBookingSystem.Application.Interfaces.Repositories;
using AirlineBookingSystem.Application.Interfaces.UnitOfWork;
using AirlineBookingSystem.Domain.Entities;
using AirlineBookingSystem.Shared.DTOs.airports;
using AirlineBookingSystem.Shared.Results;
using AutoMapper;
using FluentAssertions;
using FluentValidation;
using FluentValidation.Results;
using Moq;
using AirlineBookingSystem.UnitTests.Common.TestData;

namespace AirlineBookingSystem.UnitTests.Features.Airports.Commands.Create;

public class CreateAirportCommandHandlerTests
{
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly Mock<IValidator<CreateAirportCommand>> _validatorMock;
    private readonly CreateAirportCommandHandler _handler;
    private readonly Mock<IAirportRepository> _airportRepositoryMock;

    public CreateAirportCommandHandlerTests()
    {
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _mapperMock = new Mock<IMapper>();
        _validatorMock = new Mock<IValidator<CreateAirportCommand>>();
        _airportRepositoryMock = new Mock<IAirportRepository>();
        _unitOfWorkMock.Setup(u => u.Airports).Returns(_airportRepositoryMock.Object);
        _handler = new CreateAirportCommandHandler(_unitOfWorkMock.Object, _mapperMock.Object, _validatorMock.Object);
    }

    [Fact]
    public async Task Handle_ShouldReturnSuccess_WhenAirportIsCreatedSuccessfully()
    {
        // Arrange
        var createAirportDto = new CreateAirportDto("ABC", "Test Airport", 1, "UTC");
        var command = new CreateAirportCommand(createAirportDto);
        var airport = AirportFactory.GetAirportFaker(1).Generate();
        airport.Id = 1;
        airport.AirportCode = "ABC";
        airport.Id = 1;
        airport.AirportCode = "ABC";
        var airportDto = new AirportDto(1, "ABC", "Test Airport", 1, "UTC");

        _validatorMock.Setup(v => v.ValidateAsync(command, CancellationToken.None)).ReturnsAsync(new ValidationResult());
        _mapperMock.Setup(m => m.Map<Airport>(createAirportDto)).Returns(airport);
        _airportRepositoryMock.Setup(r => r.AddAsync(airport)).Returns(Task.CompletedTask);
        _unitOfWorkMock.Setup(u => u.CompleteAsync()).ReturnsAsync(1);
        _mapperMock.Setup(m => m.Map<AirportDto>(airport)).Returns(airportDto);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.IsSuccess.Should().BeTrue();
        result.StatusCode.Should().Be(ResultStatusCode.Created);
        result.Value.Should().Be(airportDto);
        _airportRepositoryMock.Verify(r => r.AddAsync(airport), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldReturnConflict_WhenAirportCodeAlreadyExists()
    {
        // Arrange
        var createAirportDto = new CreateAirportDto("ABC", "Test Airport", 1, "UTC");
        var command = new CreateAirportCommand(createAirportDto);
        var validationFailure = new ValidationFailure("Airport.AirportCode", "An airport with this code already exists.") { ErrorCode = "Conflict" };
        var validationResult = new ValidationResult(new[] { validationFailure });

        _validatorMock.Setup(v => v.ValidateAsync(command, CancellationToken.None)).ReturnsAsync(validationResult);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.IsSuccess.Should().BeFalse();
        result.StatusCode.Should().Be(ResultStatusCode.Conflict);
        result.Error.Should().Be("An airport with this code already exists.");
        _airportRepositoryMock.Verify(r => r.AddAsync(It.IsAny<Airport>()), Times.Never);
        _unitOfWorkMock.Verify(u => u.CompleteAsync(), Times.Never);
    }
}