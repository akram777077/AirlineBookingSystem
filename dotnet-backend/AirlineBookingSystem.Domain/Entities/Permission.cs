using System.Collections.Generic;
using AirlineBookingSystem.Shared.Enums;

namespace AirlineBookingSystem.Domain.Entities;

/// <summary>
/// Represents a permission entity.
/// </summary>
public class Permission
{
    /// <summary>
    /// Gets or sets the ID of the permission.
    /// </summary>
    public int Id { get; set; }
    /// <summary>
    /// Gets or sets the name of the permission.
    /// </summary>
    public PermissionEnum Name { get; set; }
    /// <summary>
    /// Gets or sets the description of the permission.
    /// </summary>
    public string? Description { get; set; }
    /// <summary>
    /// Gets or sets the collection of role-permission associations.
    /// </summary>
    public ICollection<RolePermission> RolePermissions { get; set; } = new List<RolePermission>();
}
