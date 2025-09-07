namespace AirlineBookingSystem.API.Routes;

using AirlineBookingSystem.API.Routes.BaseRoute;

public class RoleRoutes : Base
{
    public RoleRoutes() : base("roles") { }
    public const string GetRolePermissions = "{roleId:int}/permissions";
    public const string AssignPermissionsToRole = "{roleId:int}/permissions";
    public const string RemovePermissionFromRole = "{roleId:int}/permissions/{permissionId:int}";
}