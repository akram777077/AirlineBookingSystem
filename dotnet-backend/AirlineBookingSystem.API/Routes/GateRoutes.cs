namespace AirlineBookingSystem.API.Routes;

public static class GateRoutes
{
    public const string BaseRoute = "api/v{version:apiVersion}/gates";
    public const string GetByIdRoute = BaseRoute + "/{id:int}";
}