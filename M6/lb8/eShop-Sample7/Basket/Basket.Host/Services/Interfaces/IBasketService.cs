using Basket.Host.Models.Dtos;
using Basket.Host.Models.Request;
using Basket.Host.Models.Response;

namespace Basket.Host.Services.Interfaces
{
    public interface IBasketService
    {
        Task<AddProductResponse?> AddAsync(string key, AddProductRequest value);
        Task<GetProductsResponse> GetAsync(string key);
        Task<bool> ClearAsync(string key);
        Task<bool> RemoveProductAsync(string key, int value);
        Task<BasketProductDto?> IncrementProductAsync(string key, int value);
        Task<BasketProductDto?> DecrementProductAsync(string key, int value);
        Task Log(string key);
    }
}
