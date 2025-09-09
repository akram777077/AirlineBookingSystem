namespace AirlineBookingSystem.API.Routes;

public static class AirplaneRoutes
{
    public const string BaseRoute = "api/v{version:apiVersion}/airplanes";
    public const string GetByIdRoute = BaseRoute + "/{id:int}";
}