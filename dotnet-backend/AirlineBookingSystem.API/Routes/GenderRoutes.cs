public static class GenderRoutes
{
    public const string BaseRoute = "api/v{version:apiVersion}/genders";
    public const string GetByIdRoute = BaseRoute + "/{id:int}";
}