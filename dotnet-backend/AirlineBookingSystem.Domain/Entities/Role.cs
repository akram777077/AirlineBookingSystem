using System.Collections.Generic;
using AirlineBookingSystem.Shared.Enums;

namespace AirlineBookingSystem.Domain.Entities
{
    /// <summary>
    /// Represents a role entity.
    /// </summary>
    public class Role
    {
        /// <summary>
        /// Gets or sets the ID of the role.
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Gets or sets the name of the role.
        /// </summary>
        public RoleEnum RoleName { get; set; }
        /// <summary>
        /// Gets or sets the collection of users with this role.
        /// </summary>
        public ICollection<User> Users { get; set; } = new List<User>();
        /// <summary>
        /// Gets or sets the collection of role-permission associations.
        /// </summary>
        public ICollection<RolePermission> RolePermissions { get; set; } = new List<RolePermission>();
    }
}
