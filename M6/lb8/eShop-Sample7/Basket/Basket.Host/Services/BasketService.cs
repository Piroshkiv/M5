using Basket.Host.Models;
using Basket.Host.Models.Dtos;
using Basket.Host.Models.Request;
using Basket.Host.Models.Response;
using Basket.Host.Services.Interfaces;
using Newtonsoft.Json.Linq;

namespace Basket.Host.Services;

public class BasketService : IBasketService
{
    private readonly ICacheService _cacheService;

    public BasketService(ICacheService cacheService)
    {
        _cacheService = cacheService;
    }

    public async Task<ProductResponse?> AddAsync(string key, int id)
    {
        var result = await _cacheService.AddAsync(key, new BasketProductDto { Product = id, Quantity = 1 });

        return new ProductResponse { Product = result! };
    }

    public async Task<bool> ClearAsync(string key)
    {
        return await _cacheService.ClearAsync(key);
    }

    public async Task<ProductResponse?> DecrementProductAsync(string key, int value)
    {
        var result = await _cacheService.DecrementProductAsync(key, value);

        return new ProductResponse { Product = result! };
    }

    public async Task<BasketResponse> GetAsync(string key)
    {
        var result = await _cacheService.GetAsync(key);

        if(result == null)
        {
            return default(BasketResponse)!;
        }

        return new BasketResponse {  Products = result.Products, Size  = result.Size };
    }

    public async Task<ProductResponse?> GetProductByIdAsync(string key, int id)
    {
        var result = await _cacheService.GetProductByIdAsync(key, id);

        return new ProductResponse { Product = result! };
    }

    public async Task<ProductResponse?> IncrementProductAsync(string key, int value)
    {
        var result = await _cacheService.IncrementProductAsync(key, value);

        return new ProductResponse { Product = result! };
    }

    public async Task<bool> RemoveProductAsync(string key, int value)
    {
        return await _cacheService.RemoveProductAsync(key,value);
    }
}