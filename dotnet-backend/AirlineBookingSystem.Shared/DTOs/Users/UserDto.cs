namespace AirlineBookingSystem.Shared.DTOs.Users;

public class UserDto
{
    public required int Id { get; set; }
    public required string Username { get; set; }
    public required string Email { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required bool IsActive { get; set; }
    public required string RoleName { get; set; }
}
