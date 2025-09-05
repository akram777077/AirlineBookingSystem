using AirlineBookingSystem.Shared.DTOs.airplanes;
using AirlineBookingSystem.Shared.Filters;
using AirlineBookingSystem.Shared.Results;
using MediatR;

namespace AirlineBookingSystem.Application.Features.Airplanes.Queries.Search;

/// <summary>
/// Represents a query to search for airplanes based on a filter.
/// </summary>
/// <param name="Filter">The filter criteria for searching airplanes.</param>
using AirlineBookingSystem.Shared.DTOs.airplanes;
using AirlineBookingSystem.Shared.Filters;
using MediatR;

namespace AirlineBookingSystem.Application.Features.Airplanes.Queries.Search;

/// <summary>
/// Represents a query to search for airplanes.
/// </summary>
public class SearchAirplanesQuery : AirplaneSearchFilter, IRequest<IEnumerable<AirplaneDto>>
{
}
