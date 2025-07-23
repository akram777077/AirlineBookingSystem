namespace AirlineBookingSystem.Shared.DTOs;

public record SeatDto(int Id, int ClassTypesId, string SeatNumber, bool IsReserved, int AirplaneId);