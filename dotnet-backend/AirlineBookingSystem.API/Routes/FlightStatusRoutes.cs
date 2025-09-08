public static class FlightStatusRoutes
{
    public const string BaseRoute = "api/v{version:apiVersion}/flight-statuses";
    public const string GetByIdRoute = BaseRoute + "/{id:int}";
}