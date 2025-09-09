using AirlineBookingSystem.Application.Interfaces.UnitOfWork;
using AirlineBookingSystem.Domain.Entities;
using AirlineBookingSystem.Shared.DTOs.Cities;
using AirlineBookingSystem.Shared.Results;
using AutoMapper;
using MediatR;

namespace AirlineBookingSystem.Application.Features.Cities.Queries.GetById;

/// <summary>
/// Handles the retrieval of a city by its unique identifier.
/// </summary>
public class GetCityByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    : IRequestHandler<GetCityByIdQuery, Result<CityDto>>
{
    /// <summary>
    /// Handles the <see cref="GetCityByIdQuery"/> to retrieve a city by its ID.
    /// </summary>
    /// <param name="request">The query to handle.</param>
    /// <param name="cancellationToken">A token to observe while waiting for the task to complete.</param>
    /// <returns>A <see cref="Result{CityDto}"/> indicating the success or failure of the operation, with the city DTO on success.</returns>
    public async Task<Result<CityDto>> Handle(GetCityByIdQuery request, CancellationToken cancellationToken)
    {
        var city = await unitOfWork.Cities.GetByIdAsync(request.Id);
        return city == null ? Result.NotFound<CityDto>("City not found") : Result.Success(mapper.Map<CityDto>(city));
    }
}
