namespace AirlineBookingSystem.API.Routes;

public static class AirplaneRoutes
{
    public const string Base = "api/v{version:apiVersion}/airplanes";
    public const string GetById = "{id:int}";
}