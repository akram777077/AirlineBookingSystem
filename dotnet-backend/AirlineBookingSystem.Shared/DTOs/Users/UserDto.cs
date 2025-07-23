namespace AirlineBookingSystem.Shared.DTOs.Users;

public record UserDto(int Id, string Username, string Email, string FirstName, string LastName, bool IsActive, string RoleName);
