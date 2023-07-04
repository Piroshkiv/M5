using MVC.ViewModels.Basket;
namespace MVC.Services.Interfaces
{
    public interface IBasketService
    {
        Task<Basket> GetBasket();
    }
}
