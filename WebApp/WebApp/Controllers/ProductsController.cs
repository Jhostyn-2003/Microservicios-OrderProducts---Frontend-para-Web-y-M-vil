using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Text.Json;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public ProductsController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient("ApiGateway");
            var response = await client.GetAsync("products");

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var products = JsonSerializer.Deserialize<List<Product>>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                return View(products);
            }

            return View(new List<Product>());
        }
    }
}
