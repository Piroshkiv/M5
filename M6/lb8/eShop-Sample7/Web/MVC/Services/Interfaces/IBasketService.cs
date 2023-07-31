using MVC.ViewModels.Basket;
using MVC.Models.Response;
namespace MVC.Services.Interfaces
{
    public interface IBasketService
    {
        Task<Basket> GetBasket();
        public Task<ProductResponse> GetProductById(int id);
        public Task<bool> Clear();
        public Task<bool> Remove(int id);
        public Task<ProductResponse> Add(int id);
        public Task<ProductResponse> Increment(int id);
        public Task<ProductResponse> Decrement(int id);
    }
}
