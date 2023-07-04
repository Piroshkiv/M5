using MVC.Services.Interfaces;
using MVC.ViewModels.Basket;

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
                HttpMethod.Post,
                null);
            return result;
        }
    }
}
