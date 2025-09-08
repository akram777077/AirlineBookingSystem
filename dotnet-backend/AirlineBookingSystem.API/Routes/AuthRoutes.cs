public static class AuthRoutes
{
    public const string BaseRoute = "api/v{version:apiVersion}/auth";
    public const string Register = "register";
    public const string Login = "login";
    public const string Refresh = "refresh";
    public const string Revoke = "revoke";
    public const string GetByIdRoute = BaseRoute + "/{id:int}";
}