using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System.Text;
using Microsoft.Extensions.Configuration;
using System;
using AirlineBookingSystem.API.Routes;

using Microsoft.AspNetCore.RateLimiting;

namespace AirlineBookingSystem.API.Controllers
{
    /// <summary>
    /// Controller for handling payment-related operations, acting as a proxy to the Go payment service.
    /// </summary>
    [ApiVersion("1.0")]
    [ApiController]
    [Route(PaymentRoutes.BaseRoute)]
    [EnableRateLimiting("fixed")]
    public class PaymentController : ControllerBase
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        // ...existing code...

        /// <summary>
        /// Initializes a new instance of the <see cref="PaymentController"/> class.
        /// </summary>
        /// <param name="httpClient">The HTTP client for making requests to external services.</param>
        /// <param name="configuration">The application configuration for retrieving settings.</param>
        public PaymentController(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }

        /// <summary>
        /// Proxies a request to the Go payment service to create a Stripe Payment Intent.
        /// </summary>
        /// <param name="request">The request containing payment details like amount and currency.</param>
        /// <returns>An <see cref="IActionResult"/> containing the client secret from Stripe if successful, or an error.</returns>
        /// <response code="200">Returns the client secret for the Payment Intent.</response>
        /// <response code="500">If the Go Payment Service URL is not configured or an error occurs in the Go service.</response>
        [HttpPost(PaymentRoutes.CreatePaymentIntent)]
        public async Task<IActionResult> CreatePaymentIntent([FromBody] PaymentIntentRequest request)
        {
            var goServiceUrl = _configuration["GoPaymentService:Url"];
            if (string.IsNullOrEmpty(goServiceUrl))
            {
                return StatusCode(500, "Go Payment Service URL is not configured.");
            }

            var jsonContent = new StringContent(
                JsonSerializer.Serialize(new { amount = request.Amount, currency = request.Currency }),
                Encoding.UTF8,
                "application/json"
            );

            var response = await _httpClient.PostAsync($"{goServiceUrl}/create-payment-intent", jsonContent);

            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                return StatusCode((int)response.StatusCode, $"Error from Go service: {errorContent}");
            }

            var responseContent = await response.Content.ReadAsStringAsync();
            return Ok(JsonSerializer.Deserialize<object>(responseContent));
        }

        // DTO for the request
        public class PaymentIntentRequest
        {
            public long Amount { get; set; }
        public required string Currency { get; set; }
        }

        /// <summary>
        /// Receives payment confirmation callbacks from the Go payment service.
        /// </summary>
        /// <param name="request">The request containing the booking ID and payment intent ID.</param>
        /// <returns>An <see cref="IActionResult"/> indicating the success or failure of processing the confirmation.</returns>
        /// <response code="200">If the payment confirmation was received and processed successfully.</response>
        /// <response code="400">If the request is invalid.</response>
        /// <response code="500">If an internal server error occurs during processing.</response>
        [HttpPost(PaymentRoutes.ConfirmPayment)]
    public IActionResult ConfirmPayment([FromBody] PaymentConfirmationRequest request)
        {
            // In a real application, you would:
            // 1. Validate the request (e.g., check for a shared secret or API key from the Go service)
            // 2. Look up the booking using request.BookingId
            // 3. Verify the paymentIntentId if necessary
            // 4. Update the booking status to 'paid' in your database
            // 5. Potentially trigger further actions (e.g., send confirmation email, issue tickets)

            // For demonstration, just log and return success
            Console.WriteLine($"Received payment confirmation for Booking ID: {request.BookingId}, Payment Intent ID: {request.PaymentIntentId}");

            return Ok(new { message = "Payment confirmation received and processed." });
        }

        public class PaymentConfirmationRequest
        {
        public required int BookingId { get; set; }
        public required string PaymentIntentId { get; set; }
        }
    }
}