namespace AirlineBookingSystem.Shared.DTOs.Roles;

/// <summary>
/// Represents a role data transfer object.
/// </summary>
public struct RoleDto
{
    /// <summary>
    /// Gets or sets the ID of the role.
    /// </summary>
    public int Id { get; set; }
    /// <summary>
    /// Gets or sets the name of the role.
    /// </summary>
    public required string Name { get; set; }
}
