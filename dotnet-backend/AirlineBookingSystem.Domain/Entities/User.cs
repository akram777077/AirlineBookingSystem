using System;
using System.Collections.Generic;

namespace AirlineBookingSystem.Domain.Entities;

/// <summary>
/// Represents a user entity.
/// </summary>
public class User
{
    /// <summary>
    /// Gets or sets the ID of the user.
    /// </summary>
    public int Id { get; set; }
    /// <summary>
    /// Gets or sets the username.
    /// </summary>
    public required string Username { get; set; }
    /// <summary>
    /// Gets or sets the password hash.
    /// </summary>
    public required string Password { get; set; }
    /// <summary>
    /// Gets or sets the last login timestamp.
    /// </summary>
    public DateTime? LastLoginAt { get; set; }
    /// <summary>
    /// Gets or sets a value indicating whether the user is active.
    /// </summary>
    public bool IsActive { get; set; } = true;
    /// <summary>
    /// Gets or sets the ID of the person associated with the user.
    /// </summary>
    public int PersonId { get; set; }
    /// <summary>
    /// Gets or sets the ID of the role of the user.
    /// </summary>
    public int RoleId { get; set; }
    /// <summary>
    /// Gets or sets the creation timestamp of the user.
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    /// <summary>
    /// Gets or sets the update timestamp of the user.
    /// </summary>
    public DateTime? UpdatedAt { get; set; }
    /// <summary>
    /// Gets or sets the deletion timestamp of the user.
    /// </summary>
    public DateTime? DeletedAt { get; set; }
    /// <summary>
    /// Gets or sets the person associated with the user.
    /// </summary>
    public required Person Person { get; set; }
    /// <summary>
    /// Gets or sets the role of the user.
    /// </summary>
    public required Role Role { get; set; }
    
    /// <summary>
    /// Gets or sets the collection of user-airport associations.
    /// </summary>
    public ICollection<UserAirport> UserAirports { get; set; } = new List<UserAirport>();
    /// <summary>
    /// Gets or sets the collection of refresh tokens for the user.
    /// </summary>
    public ICollection<RefreshToken> RefreshTokens { get; set; } = new List<RefreshToken>();
}
