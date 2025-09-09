namespace AirlineBookingSystem.API.Routes;

public static class CityRoutes
{
    public const string BaseRoute = "api/v{version:apiVersion}/cities";
    public const string GetByIdRoute = BaseRoute + "/{id:int}";
}