using AirlineBookingSystem.Shared.DTOs.ClassTypes;
using AirlineBookingSystem.Shared.Results;
using MediatR;

namespace AirlineBookingSystem.Application.Features.ClassTypes.Queries.GetById;

/// <summary>
/// Represents a query to retrieve a class type by its unique identifier.
/// </summary>
/// <param name="Id">The unique identifier of the class type.</param>
public record GetClassTypeByIdQuery(int Id) : IRequest<Result<ClassTypeDto>>;
