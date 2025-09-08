namespace AirlineBookingSystem.API.Routes;



public static class FlightRoutes
{
    public const string BaseRoute = "api/v{version:apiVersion}/flights";
    public const string GetByIdRoute = BaseRoute + "/{id:int}";
    public const string Search = "search";
    public const string MarkAsDeparted = "{id:int}/mark-departed";
    public const string MarkAsArrived = "{id:int}/mark-arrived";
}