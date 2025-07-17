using AirlineBookingSystem.Shared.DTOs.airplanes;
using AirlineBookingSystem.Shared.Filters;
using AirlineBookingSystem.Shared.Results;
using MediatR;

namespace AirlineBookingSystem.Application.Features.Airplanes.Queries.Search;

public record SearchAirplanesQuery(AirplaneSearchFilter Filter) : IRequest<PagedResult<List<AirplaneDto>>>;
