namespace AirlineBookingSystem.Shared.Filters;

/// <summary>
/// Represents a filter for searching users.
/// </summary>
public class UserSearchFilter
{
    /// <summary>
    /// Gets or sets the page number.
    /// </summary>
    public int PageNumber { get; set; } = 1;
    /// <summary>
    /// Gets or sets the page size.
    /// </summary>
    public int PageSize { get; set; } = 10;
    /// <summary>
    /// Gets or sets the username to search for.
    /// </summary>
    public string? Username { get; set; }
    /// <summary>
    /// Gets or sets the email to search for.
    /// </summary>
    public string? Email { get; set; }
    /// <summary>
    /// Gets or sets a value indicating whether to search for active or inactive users.
    /// </summary>
    public bool? IsActive { get; set; }
    /// <summary>
    /// Gets or sets the ID of the role to search for.
    /// </summary>
    public int? RoleId { get; set; }
}
