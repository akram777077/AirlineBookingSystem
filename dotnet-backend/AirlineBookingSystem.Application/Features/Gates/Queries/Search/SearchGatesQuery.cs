using AirlineBookingSystem.Shared.DTOs.Gates;
using AirlineBookingSystem.Shared.Results;
using MediatR;

namespace AirlineBookingSystem.Application.Features.Gates.Queries.Search;

/// <summary>
/// Represents a query to search for gates based on a filter.
/// </summary>
/// <param name="Filter">The filter criteria for searching gates.</param>
public record SearchGatesQuery(GateSearchFilter Filter) : IRequest<PagedResult<List<GateDto>>>;

