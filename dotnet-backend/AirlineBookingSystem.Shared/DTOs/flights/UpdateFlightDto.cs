namespace AirlineBookingSystem.Shared.DTOs.flights;

public record UpdateFlightDto(DateTimeOffset DepartureTime, DateTimeOffset? ArrivalTime, int AirplaneId, int? ArrivalGateId, int DepartureGateId);