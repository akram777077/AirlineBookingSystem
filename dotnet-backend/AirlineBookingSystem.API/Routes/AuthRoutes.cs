namespace AirlineBookingSystem.API.Routes;

using AirlineBookingSystem.API.Routes.BaseRoute;

public class AuthRoutes : Base
{
    public AuthRoutes() : base("auth") { }
    public const string Register = "register";
    public const string Login = "login";
    public const string Refresh = "refresh";
    public const string Revoke = "revoke";
}