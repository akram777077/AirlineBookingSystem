namespace AirlineBookingSystem.Shared.Filters;

public class UserSearchFilter
{
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
    public string? Username { get; set; }
    public string? Email { get; set; }
    public bool? IsActive { get; set; }
    public int? RoleId { get; set; }
}
