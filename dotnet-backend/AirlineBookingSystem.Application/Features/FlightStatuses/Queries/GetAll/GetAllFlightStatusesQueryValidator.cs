using FluentValidation;

namespace AirlineBookingSystem.Application.Features.FlightStatuses.Queries.GetAll;

public class GetAllFlightStatusesQueryValidator : AbstractValidator<GetAllFlightStatusesQuery>
{
    public GetAllFlightStatusesQueryValidator()
    {
        // No specific validation rules needed for a parameterless query
    }
}