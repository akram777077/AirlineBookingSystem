namespace AirlineBookingSystem.Domain.Entities;

/// <summary>
/// Represents a role-permission association entity.
/// </summary>
public class RolePermission
{
    /// <summary>
    /// Gets or sets the ID of the role.
    /// </summary>
    public int RoleId { get; set; }
    /// <summary>
    /// Gets or sets the ID of the permission.
    /// </summary>
    public int PermissionId { get; set; }
    /// <summary>
    /// Gets or sets the role.
    /// </summary>
    public required Role Role { get; set; }
    /// <summary>
    /// Gets or sets the permission.
    /// </summary>
    public required Permission Permission { get; set; }
}
