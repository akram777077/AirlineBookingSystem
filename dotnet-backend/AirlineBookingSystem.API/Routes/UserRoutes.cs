namespace AirlineBookingSystem.API.Routes;

using AirlineBookingSystem.API.Routes.BaseRoute;

public class UserRoutes : Base
{
    public UserRoutes() : base("users") { }
    public const string Activate = "{id:int}/activate";
    public const string Deactivate = "{id:int}/deactivate";
}