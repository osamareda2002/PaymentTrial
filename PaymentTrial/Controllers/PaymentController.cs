using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Text;

namespace PaymentTrial.Controllers
{
    public class PaymentController : Controller
    {
        private readonly HttpClient _httpClient;

        public PaymentController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult>Pay()
        {
            var authToken = await GetAuthToken();
            var orderId = await CreateOrder(authToken);
            var paymentKey = await GetPaymentKey(authToken, orderId);

            var iframeUrl = $"https://accept.paymob.com/api/acceptance/iframes/861161?payment_token={paymentKey}";
            return Redirect(iframeUrl);
        }
        private async Task<string> GetAuthToken()
        {
            var request = new
            {
                api_key = "ZXlKaGJHY2lPaUpJVXpVeE1pSXNJblI1Y0NJNklrcFhWQ0o5LmV5SmpiR0Z6Y3lJNklrMWxjbU5vWVc1MElpd2ljSEp2Wm1sc1pWOXdheUk2T1RnNU9ETTRMQ0p1WVcxbElqb2lhVzVwZEdsaGJDSjkuRkZxUTUzdmRhOXgyZGVGUklpQTFURFV0Z0dlR25UX1d6akhKRkdMVTJLTFdHejBmd2pneHJWd29GRW8tY01DTk9TRi1RRU56Vk9nQ0JNcE0teFkzQ0E="
            };

            var response = await _httpClient.PostAsync("https://accept.paymob.com/api/auth/tokens", new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json"));
            var responseBody = await response.Content.ReadAsStringAsync();
            var authResponse = JsonSerializer.Deserialize<Dictionary<string, object>>(responseBody);

            return authResponse["token"].ToString();
        }

        private async Task<string> CreateOrder(string authToken)
        {
            var request = new
            {
                auth_token = authToken,
                delivery_needed = "false",
                amount_cents = "100", // Example amount in cents (1.00 EGP)
                currency = "EGP",
                items = new object[] { }
            };

            var response = await _httpClient.PostAsync("https://accept.paymob.com/api/ecommerce/orders", new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json"));
            var responseBody = await response.Content.ReadAsStringAsync();
            var orderResponse = JsonSerializer.Deserialize<Dictionary<string, object>>(responseBody);

            return orderResponse["id"].ToString();
        }

        private async Task<string> GetPaymentKey(string authToken, string orderId)
        {
            var request = new
            {
                auth_token = authToken,
                amount_cents = "100", // Match the amount with CreateOrder
                expiration = 3600,
                order_id = orderId,
                billing_data = new
                {
                    // Sample billing data
                    apartment = "803",
                    email = "example@example.com",
                    floor = "42",
                    first_name = "John",
                    street = "Sample Street",
                    building = "Sample Building",
                    phone_number = "+201000000000",
                    shipping_method = "NA",
                    postal_code = "01898",
                    city = "Cairo",
                    country = "EGY",
                    last_name = "Doe",
                    state = "CA"
                },
                currency = "EGP",
                integration_id = 4627656
            };

            var response = await _httpClient.PostAsync("https://accept.paymob.com/api/acceptance/payment_keys", new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json"));
            var responseBody = await response.Content.ReadAsStringAsync();
            var paymentKeyResponse = JsonSerializer.Deserialize<Dictionary<string, object>>(responseBody);

            return paymentKeyResponse["token"].ToString();
        }
    }
}
