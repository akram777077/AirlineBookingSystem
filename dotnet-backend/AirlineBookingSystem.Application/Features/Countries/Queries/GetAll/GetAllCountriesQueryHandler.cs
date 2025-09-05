using AirlineBookingSystem.Application.Interfaces.UnitOfWork;
using AirlineBookingSystem.Domain.Entities;
using AirlineBookingSystem.Shared.DTOs.countries;
using AirlineBookingSystem.Shared.Results;
using AutoMapper;
using MediatR;

namespace AirlineBookingSystem.Application.Features.Countries.Queries.GetAll;

/// <summary>
/// Handles the retrieval of all countries.
/// </summary>
public class GetAllCountriesQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    : IRequestHandler<GetAllCountriesQuery, Result<List<CountryDto>>>
{
    /// <summary>
    /// Handles the <see cref="GetAllCountriesQuery"/> to retrieve all countries.
    /// </summary>
    /// <param name="request">The query to handle.</param>
    /// <param name="cancellationToken">A token to observe while waiting for the task to complete.</param>
    /// <returns>A <see cref="Result{List{CountryDto}}"/> containing a list of country DTOs on success.</returns>
    public async Task<Result<List<CountryDto>>> Handle(GetAllCountriesQuery request, CancellationToken cancellationToken)
    {
        var countries = await unitOfWork.Countries.GetAllAsync();
        return Result<List<CountryDto>>.Success(mapper.Map<List<CountryDto>>(countries));
    }
}
