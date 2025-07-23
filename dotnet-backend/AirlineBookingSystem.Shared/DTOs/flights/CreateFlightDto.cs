namespace AirlineBookingSystem.Shared.DTOs.flights;

public record CreateFlightDto(DateTimeOffset DepartureTime, DateTimeOffset? ArrivalTime, int AirplaneId, int? ArrivalGateId, int DepartureGateId);
