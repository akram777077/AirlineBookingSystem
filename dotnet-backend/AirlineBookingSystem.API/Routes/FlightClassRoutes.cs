namespace AirlineBookingSystem.API.Routes;

public static class FlightClassRoutes
{
    public const string BaseRoute = "api/v{version:apiVersion}/flight-classes";
    public const string GetByIdRoute = BaseRoute + "/{id:int}";
    public const string GetByFlightId = "by-flight/{flightId:int}";
}