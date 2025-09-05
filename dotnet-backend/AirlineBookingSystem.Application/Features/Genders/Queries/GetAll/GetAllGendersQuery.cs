using AirlineBookingSystem.Shared.DTOs.Genders;
using AirlineBookingSystem.Shared.Results;
using MediatR;

namespace AirlineBookingSystem.Application.Features.Genders.Queries.GetAll;

/// <summary>
/// Represents a query to retrieve all genders.
/// </summary>
public record GetAllGendersQuery : IRequest<Result<IEnumerable<GenderDto>>>;

