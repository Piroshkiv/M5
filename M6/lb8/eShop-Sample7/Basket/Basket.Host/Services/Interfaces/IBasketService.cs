using Basket.Host.Models.Dtos;
using Basket.Host.Models.Request;
using Basket.Host.Models.Response;

namespace Basket.Host.Services.Interfaces
{
    public interface IBasketService
    {
        Task<ProductResponse?> AddAsync(string key, int value);
        Task<BasketResponse> GetAsync(string key);
        Task<bool> ClearAsync(string key);
        Task<bool> RemoveProductAsync(string key, int value);
        Task<ProductResponse?> IncrementProductAsync(string key, int value);
        Task<ProductResponse?> DecrementProductAsync(string key, int value);
        Task<ProductResponse?> GetProductByIdAsync(string key, int id);
    }
}
