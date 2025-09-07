namespace AirlineBookingSystem.API.Routes;

public static class PaymentRoutes
{
    public const string Base = "api/v{version:apiVersion}/payment";
    public const string CreatePaymentIntent = "create-payment-intent";
    public const string ConfirmPayment = "confirm-payment";

}