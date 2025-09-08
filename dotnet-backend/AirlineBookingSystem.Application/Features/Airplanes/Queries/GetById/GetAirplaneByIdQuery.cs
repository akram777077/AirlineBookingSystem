using AirlineBookingSystem.Shared.DTOs.airplanes;
using MediatR;
using AirlineBookingSystem.Shared.Results;

namespace AirlineBookingSystem.Application.Features.Airplanes.Queries.GetById;

/// <summary>
/// Represents a query to get an airplane by its ID.
/// </summary>
public class GetAirplaneByIdQuery : IRequest<Result<AirplaneDto>>
{
    public GetAirplaneByIdQuery(int id)
    {
        Id = id;
    }
    /// <summary>
    /// Gets or sets the ID of the airplane.
    /// </summary>
    public int Id { get; set; }
    // No change needed here
}