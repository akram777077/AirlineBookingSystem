using AirlineBookingSystem.Shared.DTOs.Genders;
using AirlineBookingSystem.Shared.Results;
using MediatR;

namespace AirlineBookingSystem.Application.Features.Genders.Queries.GetById;

public class GetGenderByIdQuery(int id) : IRequest<Result<GenderDto>>
{
    public int Id => id;
}
