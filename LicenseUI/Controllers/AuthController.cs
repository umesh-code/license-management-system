using LicenseUI.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace LicenseUI.Controllers
{
    public class AuthController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _config;

        public AuthController(IHttpClientFactory httpClientFactory, IConfiguration config)
        {
            _httpClientFactory = httpClientFactory;
            _config = config;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string username, string role)
        {
            var client = _httpClientFactory.CreateClient();

            // Get Base URL from appsettings
            var baseUrl = _config.GetSection("ApiGateway")["BaseUrl"];

            if (string.IsNullOrEmpty(baseUrl))
            {
                throw new Exception("API Gateway BaseUrl is missing");
            }

            // Prepare payload
            var json = JsonConvert.SerializeObject(new
            {
                username = username,
                role = role
            });

            var content = new StringContent(json, Encoding.UTF8, "application/json");

            // CALL VIA API GATEWAY (NOT DIRECT SERVICE)
            var response = await client.PostAsync($"{baseUrl}/auth/login", content);

            var result = await response.Content.ReadAsStringAsync();

            // Parse response safely
            var data = JsonConvert.DeserializeObject<TokenResponse>(result);

            if (data == null || string.IsNullOrEmpty(data.Token))
            {
                return Content("Login failed. Token not received.");
            }

            // Store session
            HttpContext.Session.SetString("token", data.Token);
            HttpContext.Session.SetString("role", role);

            return RedirectToAction("Index", "Dashboard");
        }

        [HttpPost]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login", "Auth");
        }
    }
}