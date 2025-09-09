public static class TerminalRoutes
{
    public const string BaseRoute = "api/v{version:apiVersion}/terminals";
    public const string GetByIdRoute = BaseRoute + "/{id:int}";
}