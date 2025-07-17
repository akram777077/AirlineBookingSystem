using AirlineBookingSystem.Shared.DTOs.Gates;
using AirlineBookingSystem.Shared.Results;
using MediatR;

namespace AirlineBookingSystem.Application.Features.Gates.Commands.Create;

public record CreateGateCommand(CreateGateDto Dto) : IRequest<Result<int>>;
