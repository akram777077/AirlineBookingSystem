using AirlineBookingSystem.Shared.DTOs.Gates;
using AirlineBookingSystem.Shared.Results;
using MediatR;

namespace AirlineBookingSystem.Application.Features.Gates.Queries.GetById;

public record GetGateByIdQuery(int Id) : IRequest<Result<GateDto>>;
