using AirlineBookingSystem.Shared.DTOs.Genders;
using AirlineBookingSystem.Shared.Results;
using MediatR;

namespace AirlineBookingSystem.Application.Features.Genders.Queries.GetById;

/// <summary>
/// Represents a query to retrieve a gender by its unique identifier.
/// </summary>
/// <param name="Id">The unique identifier of the gender.</param>
public record GetGenderByIdQuery(int Id) : IRequest<Result<GenderDto>>;
