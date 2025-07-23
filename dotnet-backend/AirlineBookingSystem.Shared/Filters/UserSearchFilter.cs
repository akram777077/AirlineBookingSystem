namespace AirlineBookingSystem.Shared.Filters;

public record UserSearchFilter(int PageNumber = 1, int PageSize = 10, string? Username = null, string? Email = null, bool? IsActive = null, int? RoleId = null);
