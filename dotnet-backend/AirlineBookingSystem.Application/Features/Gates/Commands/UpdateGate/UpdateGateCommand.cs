using AirlineBookingSystem.Shared.DTOs.Gates;
using AirlineBookingSystem.Shared.Results;
using MediatR;

namespace AirlineBookingSystem.Application.Features.Gates.Commands.UpdateGate;

public record UpdateGateCommand(int Id, UpdateGateDto Dto) : IRequest<Result>;
