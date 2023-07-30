using MVC.Services.Interfaces;
using MVC.ViewModels.Basket;

namespace MVC.ViewComponents
{
    public class CatalogCounterViewComponent: ViewComponent
    {
        private readonly IBasketService _basketService;
        public CatalogCounterViewComponent(IBasketService basketService)
        {
            _basketService = basketService;
        }

        public IViewComponentResult Invoke(int id)
        {
            var result = _basketService.GetProductById(id);

            if (result.IsFaulted)
            {
                View(new BasketProduct { Product = id, Quantity = 0 });
            }

            return View(result.Result.Product);
        }
    }
}
