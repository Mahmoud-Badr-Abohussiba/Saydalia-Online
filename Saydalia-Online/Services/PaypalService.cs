using Microsoft.AspNetCore.Mvc;
using Saydalia_Online.Interfaces.InterfaceServices;
using System.Text;
using System.Text.Json.Nodes;

namespace Saydalia_Online.Services
{
    public class PaypalService : IPayService
    {

        private string _paypalClientId { get; set; } = "";
        private string _paypalSecret { get; set; } = "";
        private string _paypalUrl { get; set; } = "";


        public PaypalService(IConfiguration configuration)
        {
            _paypalClientId = configuration["PaypalSettings:ClientId"]!;
            _paypalSecret = configuration["PaypalSettings:Secret"]!;
            _paypalUrl = configuration["PaypalSettings:Url"]!;

        }

        public async Task<JsonResult> CreateOrder([FromBody] JsonObject data)
        {
            var totalAmount = data?["amount"]?.ToString();
            if (totalAmount == null)
            {
                return new JsonResult(new { Id = "" });
            }

            JsonObject createOrderRequest = new JsonObject();
            createOrderRequest.Add("intent", "CAPTURE");

            JsonObject amount = new JsonObject();
            amount.Add("currency_code", "USD");
            amount.Add("value", totalAmount);

            JsonObject purchaseUnit1 = new JsonObject();
            purchaseUnit1.Add("amount", amount);


            JsonArray purchaseUnits = new JsonArray();
            purchaseUnits.Add(purchaseUnit1);

            createOrderRequest.Add("purchase_units", purchaseUnits);
            string accessToken = await GetPaypalAccessToken();

            string url = _paypalUrl + "/v2/checkout/orders";

            using (var client = new HttpClient())
            {

                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);

                var reqestMessage = new HttpRequestMessage(HttpMethod.Post, url);
                reqestMessage.Content = new StringContent(createOrderRequest.ToString(), null,
                    "application/json");

                var httpResponse = await client.SendAsync(reqestMessage);

                if (httpResponse.IsSuccessStatusCode)
                {
                    var strResponse = await httpResponse.Content.ReadAsStringAsync();
                    var jsonResponse = JsonNode.Parse(strResponse);
                    if (jsonResponse != null)
                    {
                        string paypalOrderId = jsonResponse["id"]?.ToString() ?? "";

                        return new JsonResult(new { Id = paypalOrderId });
                    }
                }

            }


            return new JsonResult(new { Id = "" });
        }


        public async Task<JsonResult> CompleteOrder([FromBody] JsonObject data)
        {
            var orderId = data["orderID"]?.ToString();
            if (orderId == null)
            {
                return new JsonResult("error");
            }

            string accessToken = await GetPaypalAccessToken();

            string url = _paypalUrl + "/v2/checkout/orders/" + orderId + "/capture";

            using (var client = new HttpClient())
            {

                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);

                var reqestMessage = new HttpRequestMessage(HttpMethod.Post, url);
                reqestMessage.Content = new StringContent("", null, "application/json");

                var httpResponse = await client.SendAsync(reqestMessage);

                if (httpResponse.IsSuccessStatusCode)
                {
                    var strResponse = await httpResponse.Content.ReadAsStringAsync();
                    var jsonResponse = JsonNode.Parse(strResponse);
                    if (jsonResponse != null)
                    {
                        string paypalOrderStatus = jsonResponse["status"]?.ToString() ?? "";
                        if (paypalOrderStatus == "COMPLETED")
                        {
                            return new JsonResult("success");
                        }
                    }
                }

            }

            return new JsonResult("error");
        }

        private async Task<string> GetPaypalAccessToken()
        {
            string accessToken = "";

            string url = _paypalUrl + "/v1/oauth2/token";

            using (var client = new HttpClient())
            {
                string credentials64 =
                    Convert.ToBase64String(Encoding.UTF8.GetBytes(_paypalClientId + ":" + _paypalSecret));

                client.DefaultRequestHeaders.Add("Authorization", "Basic " + credentials64);

                var reqestMessage = new HttpRequestMessage(HttpMethod.Post, url);
                reqestMessage.Content = new StringContent("grant_type=client_credentials", null,
                    "application/x-www-form-urlencoded");

                var httpResponse = await client.SendAsync(reqestMessage);

                if (httpResponse.IsSuccessStatusCode)
                {
                    var strResponse = await httpResponse.Content.ReadAsStringAsync();
                    var jsonResponse = JsonNode.Parse(strResponse);
                    if (jsonResponse != null)
                    {
                        accessToken = jsonResponse["access_token"]?.ToString() ?? "";
                    }
                }

            }

            return accessToken;
        }
    }
}
