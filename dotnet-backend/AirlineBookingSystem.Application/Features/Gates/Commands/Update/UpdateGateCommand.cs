using AirlineBookingSystem.Shared.DTOs.Gates;
using AirlineBookingSystem.Shared.Results;
using MediatR;

namespace AirlineBookingSystem.Application.Features.Gates.Commands.Update;

public record UpdateGateCommand(int Id, UpdateGateDto Dto) : IRequest<Result>;
