namespace AirlineBookingSystem.API.Routes;

public static class FlightClassRoutes
{
    public const string Base = "api/v{version:apiVersion}/flight-classes";
    public const string GetById = "{id:int}";
    public const string GetByFlightId = "by-flight/{flightId:int}";

}