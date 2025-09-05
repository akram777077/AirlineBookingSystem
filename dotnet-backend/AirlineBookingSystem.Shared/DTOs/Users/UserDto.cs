namespace AirlineBookingSystem.Shared.DTOs.Users;

/// <summary>
/// Represents a user data transfer object.
/// </summary>
public class UserDto
{
    /// <summary>
    /// Gets or sets the ID of the user.
    /// </summary>
    public required int Id { get; set; }
    /// <summary>
    /// Gets or sets the username.
    /// </summary>
    public required string Username { get; set; }
    /// <summary>
    /// Gets or sets the email of the user.
    /// </summary>
    public required string Email { get; set; }
    /// <summary>
    /// Gets or sets the first name of the user.
    /// </summary>
    public required string FirstName { get; set; }
    /// <summary>
    /// Gets or sets the last name of the user.
    /// </summary>
    public required string LastName { get; set; }
    /// <summary>
    /// Gets or sets a value indicating whether the user is active.
    /// </summary>
    public required bool IsActive { get; set; }
    /// <summary>
    /// Gets or sets the name of the role of the user.
    /// </summary>
    public required string RoleName { get; set; }
}
