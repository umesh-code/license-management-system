using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace LicenseUI.Controllers
{
    public class DashboardController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _config;

        public DashboardController(IHttpClientFactory httpClientFactory, IConfiguration config)
        {
            _httpClientFactory = httpClientFactory;
            _config = config;
        }

        public IActionResult Index()
        {
            return View();
        }

        // GET LICENSE
        public async Task<IActionResult> GetLicense(string tenantId)
        {
            var client = _httpClientFactory.CreateClient();
            var baseUrl = _config.GetSection("ApiGateway")["BaseUrl"];

            if (string.IsNullOrEmpty(baseUrl))
            {
                throw new Exception("API Gateway BaseURL is missing in appsettings.json");
            }

            var token = HttpContext.Session.GetString("token");
            client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", token);

            var response = await client.GetAsync($"{baseUrl}/license/{tenantId}");
            var result = await response.Content.ReadAsStringAsync();

            ViewBag.Data = result;

            return View("Index");
        }

        // GET DOCUMENT
        public async Task<IActionResult> GetDocument(string tenantId)
        {
            var client = _httpClientFactory.CreateClient();
            var baseUrl = _config.GetSection("ApiGateway")["BaseUrl"];

            if (string.IsNullOrEmpty(baseUrl))
            {
                throw new Exception("API Gateway BaseURL is missing in appsettings.json");
            }

            var token = HttpContext.Session.GetString("token");
            client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", token);

            var response = await client.GetAsync($"{baseUrl}/document/{tenantId}");
            var result = await response.Content.ReadAsStringAsync();

            ViewBag.Data = result;

            return View("Index");
        }

        // SEND NOTIFICATION
        [HttpPost]
        public async Task<IActionResult> SendNotification(string message)
        {

            var client = _httpClientFactory.CreateClient();
            var baseUrl = _config.GetSection("ApiGateway")["BaseUrl"];

            if (string.IsNullOrEmpty(message))
            {
                ViewBag.Data = "{\"error\":\"Message is required\"}";
                return View("Index");
            }

            if (string.IsNullOrEmpty(baseUrl))
            {
                throw new Exception("API Gateway BaseURL is missing in appsettings.json");
            }

            var token = HttpContext.Session.GetString("token");
            client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", token);

            var payload = new
            {
                message = message
            };


            var content = new StringContent(
               JsonConvert.SerializeObject(payload),
               System.Text.Encoding.UTF8,
               "application/json"
           );

            var response = await client.PostAsync($"{baseUrl}/notification", content);
            var result = await response.Content.ReadAsStringAsync();

            ViewBag.Data = result;
            return View("Index");
        }
    }
}