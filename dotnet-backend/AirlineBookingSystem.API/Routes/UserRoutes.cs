namespace AirlineBookingSystem.API.Routes;

public static class UserRoutes
{
    public const string Base = "api/v{version:apiVersion}/users";
    public const string GetById = "{id:int}";
    public const string Activate = "{id:int}/activate";
    public const string Deactivate = "{id:int}/deactivate";
}