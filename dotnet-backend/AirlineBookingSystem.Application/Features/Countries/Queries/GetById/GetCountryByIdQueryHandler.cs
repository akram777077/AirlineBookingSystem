using AirlineBookingSystem.Application.Interfaces.UnitOfWork;
using AirlineBookingSystem.Domain.Entities;
using AirlineBookingSystem.Shared.DTOs.countries;
using AirlineBookingSystem.Shared.Results;
using AutoMapper;
using MediatR;

namespace AirlineBookingSystem.Application.Features.Countries.Queries.GetById;

/// <summary>
/// Handles the retrieval of a country by its unique identifier.
/// </summary>
public class GetCountryByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    : IRequestHandler<GetCountryByIdQuery, Result<CountryDto>>
{
    /// <summary>
    /// Handles the <see cref="GetCountryByIdQuery"/> to retrieve a country by its ID.
    /// </summary>
    /// <param name="request">The query to handle.</param>
    /// <param name="cancellationToken">A token to observe while waiting for the task to complete.</param>
    /// <returns>A <see cref="Result{CountryDto}"/> indicating the success or failure of the operation, with the country DTO on success.</returns>
    public async Task<Result<CountryDto>> Handle(GetCountryByIdQuery request, CancellationToken cancellationToken)
    {
        var country = await unitOfWork.Countries.GetByIdAsync(request.Id);
        return country == null ? Result.NotFound<CountryDto>("Country not found") : Result.Success(mapper.Map<CountryDto>(country));
    }
}
