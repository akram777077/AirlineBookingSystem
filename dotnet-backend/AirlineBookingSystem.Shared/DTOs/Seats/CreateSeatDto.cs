namespace AirlineBookingSystem.Shared.DTOs;

public record CreateSeatDto(int ClassTypesId, string SeatNumber, bool IsReserved, int AirplaneId);