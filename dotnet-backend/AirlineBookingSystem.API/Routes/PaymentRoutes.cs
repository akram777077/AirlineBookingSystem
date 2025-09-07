namespace AirlineBookingSystem.API.Routes;

using AirlineBookingSystem.API.Routes.BaseRoute;

public class PaymentRoutes : Base
{
    public PaymentRoutes() : base("payment") { }
    public const string CreatePaymentIntent = "create-payment-intent";
    public const string ConfirmPayment = "confirm-payment";
}