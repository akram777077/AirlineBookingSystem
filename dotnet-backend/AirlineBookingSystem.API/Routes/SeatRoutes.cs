namespace AirlineBookingSystem.API.Routes;

public static class SeatRoutes
{
    public const string BaseRoute = "api/v{version:apiVersion}/seats";
    public const string GetByIdRoute = BaseRoute + "/{id:int}";
}