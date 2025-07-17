using AirlineBookingSystem.Application.Features.Terminals.Queries.GetById;
using AirlineBookingSystem.Application.Interfaces.UnitOfWork;
using AirlineBookingSystem.Domain.Entities;
using AirlineBookingSystem.Shared.DTOs.terminals;
using AirlineBookingSystem.Shared.Enums;
using AirlineBookingSystem.Shared.Results;
using AirlineBookingSystem.UnitTests.Common.TestData;
using AutoMapper;
using FluentAssertions;
using Moq;
using Xunit;

namespace AirlineBookingSystem.UnitTests.Features.Terminals.Queries.GetTerminalById;

public class GetTerminalByIdQueryHandlerTests
{
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly GetTerminalByIdQueryHandler _handler;

    public GetTerminalByIdQueryHandlerTests()
    {
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _mapperMock = new Mock<IMapper>();
        _handler = new GetTerminalByIdQueryHandler(_unitOfWorkMock.Object, _mapperMock.Object);
    }

    [Fact]
    public async Task Handle_ShouldReturnTerminalDto_WhenTerminalExists()
    {
        // Arrange
        var terminalId = 1;
        var query = new GetTerminalByIdQuery(terminalId);

        var terminal = TerminalFactory.GetTerminalFaker(1).Generate();
        var terminalDto = new TerminalDto(terminal.Id, terminal.Name, terminal.AirportId);

        _unitOfWorkMock.Setup(u => u.Terminals.GetByIdAsync(terminalId))
            .ReturnsAsync(terminal);
        _mapperMock.Setup(m => m.Map<TerminalDto>(terminal))
            .Returns(terminalDto);

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.IsSuccess.Should().BeTrue();
        result.StatusCode.Should().Be(ResultStatusCode.Success);
        result.Value.Should().BeEquivalentTo(terminalDto);
    }

    [Fact]
    public async Task Handle_ShouldReturnFailure_WhenTerminalDoesNotExist()
    {
        // Arrange
        var terminalId = 99;
        var query = new GetTerminalByIdQuery(terminalId);

        _unitOfWorkMock.Setup(u => u.Terminals.GetByIdAsync(terminalId))
            .ReturnsAsync((Terminal)null);

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.IsSuccess.Should().BeFalse();
        result.StatusCode.Should().Be(ResultStatusCode.NotFound);
        result.Error.Should().Be("Terminal not found");
    }
}
