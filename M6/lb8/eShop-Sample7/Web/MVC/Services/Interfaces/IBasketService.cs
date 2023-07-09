using MVC.ViewModels.Basket;
namespace MVC.Services.Interfaces
{
    public interface IBasketService
    {
        Task<Basket> GetBasket();
        public Task<BasketProduct> GetProductById(int id);
        public Task<bool> Clear();
        public Task<bool> Remove(int id);
        public Task<BasketProduct> Increment(int id);
        public Task<BasketProduct> Decrement(int id);
    }
}
