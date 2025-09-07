namespace AirlineBookingSystem.API.Routes;

public static class AuthRoutes
{
    public const string Base = "api/v{version:apiVersion}/auth";
    public const string Register = "register";
    public const string Login = "login";
    public const string Refresh = "refresh";
    public const string Revoke = "revoke";
}