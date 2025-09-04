using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System.Text;
using Microsoft.Extensions.Configuration;
using System;

namespace AirlineBookingSystem.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PaymentController : ControllerBase
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public PaymentController(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }

        [HttpPost("create-payment-intent")]
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
            public string Currency { get; set; }
        }

        [HttpPost("confirm-payment")]
        public async Task<IActionResult> ConfirmPayment([FromBody] PaymentConfirmationRequest request)
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
            public string BookingId { get; set; } // Assuming a string ID for booking
            public string PaymentIntentId { get; set; }
        }
    }
}