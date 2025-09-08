namespace AirlineBookingSystem.API.Routes;

public static class RoleRoutes
{
    public const string BaseRoute = "api/v{version:apiVersion}/roles";
    public const string GetByIdRoute = BaseRoute + "/{id:int}";
    public const string GetRolePermissions = "{roleId:int}/permissions";
    public const string AssignPermissionsToRole = "{roleId:int}/permissions";
    public const string RemovePermissionFromRole = "{roleId:int}/permissions/{permissionId:int}";
}