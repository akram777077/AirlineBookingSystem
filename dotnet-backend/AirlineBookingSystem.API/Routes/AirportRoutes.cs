namespace AirlineBookingSystem.API.Routes;

public static class AirportRoutes
{
    public const string BaseRoute = "api/v{version:apiVersion}/airports";
    public const string Search = "search";
    public const string GetByIdRoute = BaseRoute + "/{id:int}";
}