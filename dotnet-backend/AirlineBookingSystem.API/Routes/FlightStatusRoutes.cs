namespace AirlineBookingSystem.API.Routes;

public static class FlightStatusRoutes
{
    public const string Base = "api/v{version:apiVersion}/flight-statuses";
    public const string GetById = "{id:int}";

}