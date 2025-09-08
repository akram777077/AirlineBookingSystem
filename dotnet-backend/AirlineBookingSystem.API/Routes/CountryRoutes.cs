namespace AirlineBookingSystem.API.Routes;

public static class CountryRoutes
{
    public const string BaseRoute = "api/v{version:apiVersion}/countries";
    public const string GetByIdRoute = BaseRoute + "/{id:int}";
}