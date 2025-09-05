namespace AirlineBookingSystem.Shared.DTOs.Permissions;

/// <summary>
/// Represents a permission data transfer object.
/// </summary>
public struct PermissionDto
{
    /// <summary>
    /// Gets or sets the ID of the permission.
    /// </summary>
    public int Id { get; set; }
    /// <summary>
    /// Gets or sets the name of the permission.
    /// </summary>
    public required string Name { get; set; }
}
