using AirlineBookingSystem.Shared.DTOs.airplanes;
using AirlineBookingSystem.Shared.Filters;
using AirlineBookingSystem.Shared.Results;
using MediatR;
using System.Collections.Generic;

namespace AirlineBookingSystem.Application.Features.Airplanes.Queries.SearchAirplanes;

public record SearchAirplanesQuery(AirplaneSearchFilter Filter) : IRequest<PagedResult<List<AirplaneDto>>>;
