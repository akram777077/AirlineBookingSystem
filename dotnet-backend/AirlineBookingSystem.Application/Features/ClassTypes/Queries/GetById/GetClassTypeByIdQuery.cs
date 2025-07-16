using AirlineBookingSystem.Shared.DTOs.ClassTypes;
using AirlineBookingSystem.Shared.Results;
using MediatR;

namespace AirlineBookingSystem.Application.Features.ClassTypes.Queries.GetById;

public class GetClassTypeByIdQuery(int id) : IRequest<Result<ClassTypeDto>>
{
    public int Id => id;
}
