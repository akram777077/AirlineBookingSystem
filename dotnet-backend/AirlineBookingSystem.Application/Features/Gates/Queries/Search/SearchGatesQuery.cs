using AirlineBookingSystem.Shared.DTOs.Gates;
using AirlineBookingSystem.Shared.Results;
using MediatR;

namespace AirlineBookingSystem.Application.Features.Gates.Queries.Search;

public record SearchGatesQuery(GateSearchFilter Filter) : IRequest<PagedResult<List<GateDto>>>;
