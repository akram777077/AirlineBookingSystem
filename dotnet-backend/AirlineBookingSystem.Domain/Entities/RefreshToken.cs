using System;

namespace AirlineBookingSystem.Domain.Entities;

/// <summary>
/// Represents a refresh token entity.
/// </summary>
public class RefreshToken
{
    /// <summary>
    /// Gets or sets the ID of the refresh token.
    /// </summary>
    public int Id { get; set; }
    /// <summary>
    /// Gets or sets the ID of the user associated with the refresh token.
    /// </summary>
    public int UserId { get; set; }
    /// <summary>
    /// Gets or sets the token string.
    /// </summary>
    public string Token { get; set; }
    /// <summary>
    /// Gets or sets the expiration date of the token.
    /// </summary>
    public DateTime Expires { get; set; }
    /// <summary>
    /// Gets a value indicating whether the token is expired.
    /// </summary>
    public bool IsExpired => DateTime.UtcNow >= Expires;
    /// <summary>
    /// Gets or sets the creation date of the token.
    /// </summary>
    public DateTime Created { get; set; }
    /// <summary>
    /// Gets or sets the revocation date of the token.
    /// </summary>
    public DateTime? Revoked { get; set; }
    /// <summary>
    /// Gets a value indicating whether the token is active.
    /// </summary>
    public bool IsActive => Revoked == null && !IsExpired;

    /// <summary>
    /// Gets or sets the user associated with the refresh token.
    /// </summary>
    public User User { get; set; }
}
