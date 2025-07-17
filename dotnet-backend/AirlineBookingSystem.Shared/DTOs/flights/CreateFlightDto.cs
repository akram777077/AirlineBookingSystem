namespace AirlineBookingSystem.Shared.DTOs.flights;

public struct CreateFlightDto
{
    public DateTimeOffset DepartureTime { get; set; }
    public DateTimeOffset? ArrivalTime { get; set; }
    public int AirplaneId { get; set; }
    public int? ArrivalGateId { get; set; }
    public int DepartureGateId { get; set; }
}
