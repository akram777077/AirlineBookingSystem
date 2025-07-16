using FluentValidation;

namespace AirlineBookingSystem.Application.Features.BookingStatuses.Queries.GetAll;

public class GetAllBookingStatusesQueryValidator : AbstractValidator<GetAllBookingStatusesQuery>
{
    public GetAllBookingStatusesQueryValidator()
    {
    }
}
