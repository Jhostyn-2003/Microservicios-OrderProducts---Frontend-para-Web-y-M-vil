using System.Net.Http;
using System.Text.Json;
using MobileApp.Models;

namespace MobileApp
{
    public partial class MainPage : ContentPage
    {
        private readonly IHttpClientFactory _httpClientFactory;

        // Constructor sin parámetros
        public MainPage()
        {
            InitializeComponent();
        }

        // Constructor con IHttpClientFactory
        public MainPage(IHttpClientFactory httpClientFactory)
        {
            InitializeComponent();
            _httpClientFactory = httpClientFactory;
            LoadProducts();
        }

        private async void LoadProducts()
        {
            var client = _httpClientFactory.CreateClient("ApiGateway");
            var response = await client.GetAsync("products");

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var products = JsonSerializer.Deserialize<List<Product>>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                ProductsListView.ItemsSource = products;
            }
        }
    }
}
