using MVC.Models.Requests;
using MVC.Services.Interfaces;
using MVC.ViewModels.Order;

namespace MVC.Services
{
    public class OrderServer : IOrderService
    {

        private readonly IOptions<AppSettings> _settings;
        private readonly IHttpClientService _httpClient;
        private readonly ILogger<CatalogService> _logger;

        public OrderServer(IHttpClientService httpClient, ILogger<CatalogService> logger, IOptions<AppSettings> settings)
        {
            _httpClient = httpClient;
            _settings = settings;
            _logger = logger;
        }

        public async Task<OrderInfo> AddOrder(OrderInfoRequest product)
        {
            var result = await _httpClient.SendAsync<OrderInfo, OrderInfoRequest>($"{_settings.Value.OrderInfoUrl}/Add",
                HttpMethod.Post,
                product);

            return result;
        }

        public async Task<OrderProduct> AddProduct(OrderProductRequest product)
        {
            var result = await _httpClient.SendAsync<OrderProduct, OrderProductRequest>($"{_settings.Value.OrderProductUrl}/Add",
                HttpMethod.Post,
                product);

            return result;
        }

        public async Task<OrderInfo> OrderById(int id)
        {
            var result = await _httpClient.SendAsync<OrderInfo, int?>($"{_settings.Value.OrderUrl}/OrderById",
                HttpMethod.Get,
                id);

            return result;
        }

        public async Task<OrderInfo> Orders()
        {
            var result = await _httpClient.SendAsync<OrderInfo, object?>($"{_settings.Value.OrderUrl}/Orders",
                HttpMethod.Get,
                null);

            return result;
        }

        public async Task<OrderInfo> SetFormed(int orderId)
        {
            var result = await _httpClient.SendAsync<OrderInfo, int?>($"{_settings.Value.OrderProductUrl}/SetFormed",
                HttpMethod.Patch,
                orderId);

            return result;
        }
    }
}
