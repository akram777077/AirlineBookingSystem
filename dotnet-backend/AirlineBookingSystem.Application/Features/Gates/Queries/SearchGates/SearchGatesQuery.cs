using AirlineBookingSystem.Shared.DTOs.Gates;
using AirlineBookingSystem.Shared.Filters;
using AirlineBookingSystem.Shared.Results;
using MediatR;

namespace AirlineBookingSystem.Application.Features.Gates.Queries.SearchGates;

public record SearchGatesQuery(GateSearchFilter Filter) : IRequest<PagedResult<List<GateDto>>>;
