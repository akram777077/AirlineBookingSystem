using AirlineBookingSystem.Shared.DTOs.ClassTypes;
using MediatR;
using AirlineBookingSystem.Shared.Results;

namespace AirlineBookingSystem.Application.Features.ClassTypes.Queries.GetAll;

/// <summary>
/// Represents a query to retrieve all class types.
/// </summary>
public record GetAllClassTypesQuery : IRequest<Result<IEnumerable<ClassTypeDto>>>;

