namespace AirlineBookingSystem.API.Routes;

public static class UserRoutes
{
    public const string BaseRoute = "api/v{version:apiVersion}/users";
    public const string GetByIdRoute = BaseRoute + "/{id:int}";
    public const string Activate = "{id:int}/activate";
    public const string Deactivate = "{id:int}/deactivate";
}
