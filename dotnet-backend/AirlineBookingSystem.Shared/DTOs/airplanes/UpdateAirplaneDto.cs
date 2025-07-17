
namespace AirlineBookingSystem.Shared.DTOs.airplanes;

public struct UpdateAirplaneDto
{
    public required string Model { get; set; }
    public required string Manufacturer { get; set; }
    public int Capacity { get; set; }
    public required string Code { get; set; }
}
