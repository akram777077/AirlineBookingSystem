using AirlineBookingSystem.Application.Features.Airports.Commands.Update;
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

namespace AirlineBookingSystem.UnitTests.Features.Airports.Commands.Update;

public class UpdateAirportCommandHandlerTests
{
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly Mock<IValidator<UpdateAirportCommand>> _validatorMock;
    private readonly UpdateAirportCommandHandler _handler;
    private readonly Mock<IAirportRepository> _airportRepositoryMock;

    public UpdateAirportCommandHandlerTests()
    {
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _mapperMock = new Mock<IMapper>();
        _validatorMock = new Mock<IValidator<UpdateAirportCommand>>();
        _airportRepositoryMock = new Mock<IAirportRepository>();
        _unitOfWorkMock.Setup(u => u.Airports).Returns(_airportRepositoryMock.Object);
        _handler = new UpdateAirportCommandHandler(_unitOfWorkMock.Object, _mapperMock.Object, _validatorMock.Object);
    }

    [Fact]
    public async Task Handle_ShouldReturnSuccess_WhenAirportIsUpdatedSuccessfully()
    {
        // Arrange
        var updateAirportDto = new UpdateAirportDto(1, "ABC", "Updated Airport", 1, "UTC");
        var command = new UpdateAirportCommand(updateAirportDto);
        var existingAirport = AirportFactory.GetAirportFaker(1).Generate();
        existingAirport.Id = 1;
        existingAirport.AirportCode = "ABC";
        existingAirport.Id = 1;
        existingAirport.AirportCode = "ABC";
        var airportDto = new AirportDto(1, "ABC", "Updated Airport", 1, "UTC");

        _validatorMock.Setup(v => v.ValidateAsync(command, CancellationToken.None)).ReturnsAsync(new ValidationResult());
        _airportRepositoryMock.Setup(r => r.GetByIdAsync(updateAirportDto.Id)).ReturnsAsync(existingAirport);
        _mapperMock.Setup(m => m.Map(updateAirportDto, existingAirport));
        _airportRepositoryMock.Setup(r => r.Update(existingAirport));
        _unitOfWorkMock.Setup(u => u.CompleteAsync()).ReturnsAsync(1);
        _mapperMock.Setup(m => m.Map<AirportDto>(existingAirport)).Returns(airportDto);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.IsSuccess.Should().BeTrue();
        result.StatusCode.Should().Be(ResultStatusCode.Success);
        result.Value.Should().Be(airportDto);
        _airportRepositoryMock.Verify(r => r.Update(existingAirport), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldReturnNotFound_WhenAirportNotFound()
    {
        // Arrange
        var updateAirportDto = new UpdateAirportDto(1, "ABC", "Test Airport", 1, "UTC");
        var command = new UpdateAirportCommand(updateAirportDto);

        _validatorMock.Setup(v => v.ValidateAsync(command, CancellationToken.None)).ReturnsAsync(new ValidationResult());
        _airportRepositoryMock.Setup(r => r.GetByIdAsync(updateAirportDto.Id)).ReturnsAsync((Airport)null);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.IsSuccess.Should().BeFalse();
        result.StatusCode.Should().Be(ResultStatusCode.NotFound);
        result.Error.Should().Be("Airport not found.");
        _airportRepositoryMock.Verify(r => r.Update(It.IsAny<Airport>()), Times.Never);
        _unitOfWorkMock.Verify(u => u.CompleteAsync(), Times.Never);
    }

    [Fact]
    public async Task Handle_ShouldReturnConflict_WhenAirportCodeAlreadyExists()
    {
        // Arrange
        var updateAirportDto = new UpdateAirportDto(1, "ABC", "Test Airport", 1, "UTC");
        var command = new UpdateAirportCommand(updateAirportDto);
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
        _airportRepositoryMock.Verify(r => r.Update(It.IsAny<Airport>()), Times.Never);
        _unitOfWorkMock.Verify(u => u.CompleteAsync(), Times.Never);
    }
}