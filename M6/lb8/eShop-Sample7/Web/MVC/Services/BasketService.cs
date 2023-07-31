using MVC.Models.Requests;
using MVC.Models.Response;
using MVC.Services.Interfaces;
using MVC.ViewModels.Basket;
using Newtonsoft.Json.Linq;

namespace MVC.Services
{
    public class BasketService : IBasketService
    {
        private readonly IOptions<AppSettings> _settings;
        private readonly IHttpClientService _httpClient;
        private readonly ILogger<CatalogService> _logger;

        public BasketService(IHttpClientService httpClient, ILogger<CatalogService> logger, IOptions<AppSettings> settings)
        {
            _httpClient = httpClient;
            _settings = settings;
            _logger = logger;
        }
        public async Task<Basket> GetBasket()
        {
            var result = await _httpClient.SendAsync<Basket, object?>($"{_settings.Value.BasketUrl}/basket",
                HttpMethod.Get,
                null);
            return result;
        }
        public async Task<ProductResponse> GetProductById(int id)
        {
            var result = await _httpClient.SendAsync<ProductResponse, object?>($"{_settings.Value.BasketUrl}/productById",
                HttpMethod.Get,
                id);
            return result;
        }
        public async Task<bool> Clear()
        {
            var result = await _httpClient.SendAsync<bool, object?>($"{_settings.Value.BasketProductUrl}/clear",
                HttpMethod.Delete,
                null);
            return result;
        }
        public async Task<bool> Remove(int id)
        {
            var result = await _httpClient.SendAsync<bool, DataRequest<int>>($"{_settings.Value.BasketProductUrl}/remove",
                HttpMethod.Delete,
                new DataRequest<int> { Value = id });
            return result;
        }
        public async Task<ProductResponse> Increment(int id)
        {
            var result = await _httpClient.SendAsync<ProductResponse, DataRequest<int>?>($"{_settings.Value.BasketProductUrl}/increment",
                HttpMethod.Post,
                new DataRequest<int> { Value = id });
            return result;
        }
        public async Task<ProductResponse> Decrement(int id)
        {
            var result = await _httpClient.SendAsync<ProductResponse, DataRequest<int>?>($"{_settings.Value.BasketProductUrl}/decrement",
            HttpMethod.Post,
                new DataRequest<int> { Value = id });
            return result;
        }

        public async Task<ProductResponse> Add(int id)
        {
            var result = await _httpClient.SendAsync<ProductResponse, DataRequest<int>?>($"{_settings.Value.BasketProductUrl}/add",
                HttpMethod.Post,
                new DataRequest<int> { Value = id });
            return result;
        }
    }
}
