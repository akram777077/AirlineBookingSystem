using AirlineBookingSystem.Shared.DTOs.airplanes;
using AirlineBookingSystem.Shared.Filters;
using AirlineBookingSystem.Shared.Results;
using MediatR;

namespace AirlineBookingSystem.Application.Features.Airplanes.Queries.Search;

/// <summary>
/// Represents a query to search for airplanes.
/// </summary>
public class SearchAirplanesQuery : AirplaneSearchFilter, IRequest<Result<PagedResult<List<AirplaneDto>>>>
{
	public SearchAirplanesQuery(string? model = null, string? manufacturer = null, int? minCapacity = null, int? maxCapacity = null, string? code = null)
	{
		Model = model;
		Manufacturer = manufacturer;
		MinCapacity = minCapacity;
		MaxCapacity = maxCapacity;
		Code = code;
	}

	public SearchAirplanesQuery(AirplaneSearchFilter filter)
	{
		Model = filter.Model;
		Manufacturer = filter.Manufacturer;
		MinCapacity = filter.MinCapacity;
		MaxCapacity = filter.MaxCapacity;
		Code = filter.Code;
		PageNumber = filter.PageNumber;
		PageSize = filter.PageSize;
	}
}