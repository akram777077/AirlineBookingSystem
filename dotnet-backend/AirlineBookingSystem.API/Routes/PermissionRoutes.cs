namespace AirlineBookingSystem.API.Routes;

public static class PermissionRoutes
{
    public const string BaseRoute = "api/v{version:apiVersion}/permissions";
    public const string GetByIdRoute = BaseRoute + "/{id:int}";
}