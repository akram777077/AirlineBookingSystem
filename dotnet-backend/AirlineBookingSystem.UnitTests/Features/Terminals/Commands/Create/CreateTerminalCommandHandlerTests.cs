using AirlineBookingSystem.Application.Features.Terminals.Commands.Create;
using AirlineBookingSystem.Application.Interfaces.UnitOfWork;
using AirlineBookingSystem.Domain.Entities;
using AirlineBookingSystem.Shared.DTOs.terminals;
using AirlineBookingSystem.Shared.Results;
using AirlineBookingSystem.UnitTests.Common.TestData;
using AutoMapper;
using FluentAssertions;
using Moq;

namespace AirlineBookingSystem.UnitTests.Features.Terminals.Commands.Create;

public class CreateTerminalCommandHandlerTests
{
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly CreateTerminalCommandHandler _handler;

    public CreateTerminalCommandHandlerTests()
    {
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _mapperMock = new Mock<IMapper>();
        _handler = new CreateTerminalCommandHandler(_unitOfWorkMock.Object, _mapperMock.Object);
    }

    [Fact]
    public async Task Handle_ShouldReturnSuccess_WhenTerminalIsCreatedSuccessfully()
    {
        // Arrange
        var createTerminalDto = new CreateTerminalDto { Name = "Terminal A", AirportId = 1 };
        var command = new CreateTerminalCommand(createTerminalDto);

        var airport = AirportFactory.GetAirportFaker(1).Generate(); // Provide a cityId
        var terminal = TerminalFactory.GetTerminalFaker(createTerminalDto.AirportId).Generate();

        _unitOfWorkMock.Setup(u => u.Airports.GetByIdAsync(createTerminalDto.AirportId))
            .ReturnsAsync(airport);
        _mapperMock.Setup(m => m.Map<Terminal>(createTerminalDto))
            .Returns(terminal);
        _unitOfWorkMock.Setup(u => u.Terminals.AddAsync(terminal))
            .Returns(Task.CompletedTask);
        _unitOfWorkMock.Setup(u => u.CompleteAsync())
            .ReturnsAsync(1);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.IsSuccess.Should().BeTrue();
        result.StatusCode.Should().Be(ResultStatusCode.Created);
        result.Value.Should().Be(terminal.Id);
        _unitOfWorkMock.Verify(u => u.Terminals.AddAsync(terminal), Times.Once);
        _unitOfWorkMock.Verify(u => u.CompleteAsync(), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldReturnFailure_WhenAirportNotFound()
    {
        // Arrange
        var createTerminalDto = new CreateTerminalDto { Name = "Terminal A", AirportId = 99 };
        var command = new CreateTerminalCommand(createTerminalDto);

        _unitOfWorkMock.Setup(u => u.Airports.GetByIdAsync(createTerminalDto.AirportId))
            .ReturnsAsync((Airport?)null);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.IsSuccess.Should().BeFalse();
        result.StatusCode.Should().Be(ResultStatusCode.NotFound);
        result.Error.Should().Be("Airport not found");
        _unitOfWorkMock.Verify(u => u.Terminals.AddAsync(It.IsAny<Terminal>()), Times.Never);
        _unitOfWorkMock.Verify(u => u.CompleteAsync(), Times.Never);
    }
}