using Basket.Host.Models.Dtos;
using Basket.Host.Models.Response;

namespace Basket.Host.Services.Interfaces
{
    public interface ICacheService
    {
        Task<BasketProductDto?> AddAsync(string key, BasketProductDto value);
        Task<BasketDto> GetAsync(string key);
        Task<bool> ClearAsync(string key);
        Task<bool> RemoveProductAsync(string key, int value);
        Task<BasketProductDto?> IncrementProductAsync(string key, int value);
        Task<BasketProductDto?> DecrementProductAsync(string key, int value);
        void Log(string message);
    }
}