using Basket.Host.Models;
using Basket.Host.Models.Dtos;
using Basket.Host.Models.Request;
using Basket.Host.Models.Response;
using Basket.Host.Services.Interfaces;

namespace Basket.Host.Services;

public class BasketService : IBasketService
{
    private readonly ICacheService _cacheService;

    public BasketService(ICacheService cacheService)
    {
        _cacheService = cacheService;
    }

    public async Task<AddProductResponse?> AddAsync(string key, AddProductRequest value)
    {
        var result = await _cacheService.AddAsync(key, new BasketProductDto { Product = value.Id, Quantity = 1 });

        if (result == null)
        {
            return default(AddProductResponse)!;
        }

        return new AddProductResponse { Product = result! };
    }

    public async Task<bool> ClearAsync(string key)
    {
        return await _cacheService.ClearAsync(key);
    }

    public async Task<BasketProductDto?> DecrementProductAsync(string key, int value)
    {
        return await _cacheService.DecrementProductAsync(key, value);
    }

    public async Task<ProductsResponse> GetAsync(string key)
    {
        var result = await _cacheService.GetAsync(key);

        if(result == null)
        {
            return default(ProductsResponse)!;
        }

        return new ProductsResponse {  Products = result.Products, Size  = result.Size };
    }

    public Task<BasketProductDto?> GetProductByIdAsync(string key, int id)
    {
        throw new NotImplementedException();
    }

    public async Task<BasketProductDto?> IncrementProductAsync(string key, int value)
    {
        return await _cacheService.IncrementProductAsync(key, value);
    }

    public async Task Log(string key)
    {
        await Task.Run(() => { _cacheService.Log($"Log: {key}"); });
    }

    public async Task<bool> RemoveProductAsync(string key, int value)
    {
        return await _cacheService.RemoveProductAsync(key,value);
    }
}