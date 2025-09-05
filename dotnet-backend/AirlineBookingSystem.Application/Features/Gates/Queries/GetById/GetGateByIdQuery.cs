using AirlineBookingSystem.Shared.DTOs.Gates;
using AirlineBookingSystem.Shared.Results;
using MediatR;

namespace AirlineBookingSystem.Application.Features.Gates.Queries.GetById;

/// <summary>
/// Represents a query to retrieve a gate by its unique identifier.
/// </summary>
/// <param name="Id">The unique identifier of the gate.</param>
public record GetGateByIdQuery(int Id) : IRequest<Result<GateDto>>;

