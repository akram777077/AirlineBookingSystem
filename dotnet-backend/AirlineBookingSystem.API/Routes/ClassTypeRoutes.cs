public static class ClassTypeRoutes
{
    public const string BaseRoute = "api/v{version:apiVersion}/class-types";
    public const string GetByIdRoute = BaseRoute + "/{id:int}";
}