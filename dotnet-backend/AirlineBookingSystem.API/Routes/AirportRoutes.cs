namespace AirlineBookingSystem.API.Routes;

public static class AirportRoutes
{
    public const string Base = "api/v{version:apiVersion}/airports";
    public const string GetById = "{id:int}";
    public const string Search = "search";

}