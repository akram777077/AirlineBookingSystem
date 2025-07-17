using AirlineBookingSystem.Application.Features.Terminals.Commands.Update;
using AirlineBookingSystem.Application.Interfaces.UnitOfWork;
using AirlineBookingSystem.Domain.Entities;
using AirlineBookingSystem.Shared.DTOs.terminals;
using AirlineBookingSystem.Shared.Results;
using AirlineBookingSystem.UnitTests.Common.TestData;
using AutoMapper;
using FluentAssertions;
using Moq;

namespace AirlineBookingSystem.UnitTests.Features.Terminals.Commands.Update;

public class UpdateTerminalCommandHandlerTests
{
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly UpdateTerminalCommandHandler _handler;

    public UpdateTerminalCommandHandlerTests()
    {
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _mapperMock = new Mock<IMapper>();
        _handler = new UpdateTerminalCommandHandler(_unitOfWorkMock.Object, _mapperMock.Object);
    }

    [Fact]
    public async Task Handle_ShouldReturnSuccess_WhenTerminalIsUpdatedSuccessfully()
    {
        // Arrange
        var existingTerminal = TerminalFactory.GetTerminalFaker(1).Generate();
        var updateTerminalDto = new UpdateTerminalDto { Id = existingTerminal.Id, Name = "Updated Terminal Name", AirportId = 2 };
        var command = new UpdateTerminalCommand(updateTerminalDto);

        var airport = AirportFactory.GetAirportFaker(1).Generate(); // Provide a cityId

        _unitOfWorkMock.Setup(u => u.Terminals.GetByIdAsync(updateTerminalDto.Id))
            .ReturnsAsync(existingTerminal);
        _unitOfWorkMock.Setup(u => u.Airports.GetByIdAsync(updateTerminalDto.AirportId))
            .ReturnsAsync(airport);
        _mapperMock.Setup(m => m.Map(updateTerminalDto, existingTerminal));
        _unitOfWorkMock.Setup(u => u.Terminals.Update(existingTerminal));
        _unitOfWorkMock.Setup(u => u.CompleteAsync())
            .ReturnsAsync(1);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.IsSuccess.Should().BeTrue();
        result.StatusCode.Should().Be(ResultStatusCode.NoContent);
        _unitOfWorkMock.Verify(u => u.Terminals.Update(existingTerminal), Times.Once);
        _unitOfWorkMock.Verify(u => u.CompleteAsync(), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldReturnFailure_WhenTerminalNotFound()
    {
        // Arrange
        var updateTerminalDto = new UpdateTerminalDto { Id = 99, Name = "Updated Terminal Name", AirportId = 1 };
        var command = new UpdateTerminalCommand(updateTerminalDto);

        _unitOfWorkMock.Setup(u => u.Terminals.GetByIdAsync(updateTerminalDto.Id))
            .ReturnsAsync((Terminal?)null);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.IsSuccess.Should().BeFalse();
        result.StatusCode.Should().Be(ResultStatusCode.NotFound);
        result.Error.Should().Be("Terminal not found");
        _unitOfWorkMock.Verify(u => u.Terminals.Update(It.IsAny<Terminal>()), Times.Never);
        _unitOfWorkMock.Verify(u => u.CompleteAsync(), Times.Never);
    }

    [Fact]
    public async Task Handle_ShouldReturnFailure_WhenAirportNotFound()
    {
        // Arrange
        var existingTerminal = TerminalFactory.GetTerminalFaker(1).Generate();
        var updateTerminalDto = new UpdateTerminalDto { Id = existingTerminal.Id, Name = "Updated Terminal Name", AirportId = 99 };
        var command = new UpdateTerminalCommand(updateTerminalDto);

        _unitOfWorkMock.Setup(u => u.Terminals.GetByIdAsync(updateTerminalDto.Id))
            .ReturnsAsync(existingTerminal);
        _unitOfWorkMock.Setup(u => u.Airports.GetByIdAsync(updateTerminalDto.AirportId))
            .ReturnsAsync((Airport?)null);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.IsSuccess.Should().BeFalse();
        result.StatusCode.Should().Be(ResultStatusCode.NotFound);
        result.Error.Should().Be("Airport not found");
        _unitOfWorkMock.Verify(u => u.Terminals.Update(It.IsAny<Terminal>()), Times.Never);
        _unitOfWorkMock.Verify(u => u.CompleteAsync(), Times.Never);
    }
}