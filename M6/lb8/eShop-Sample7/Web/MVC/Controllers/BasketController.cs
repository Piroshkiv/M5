using MVC.Services.Interfaces;
using MVC.ViewModels;
using MVC.ViewModels.Basket;

namespace MVC.Controllers
{
    public class BasketController : Controller
    {
        private readonly ICatalogService _catalogService;
        private readonly IBasketService _basketService;
        private readonly IOrderService _orderService;
        private readonly ILogger<AccountController> _logger;

        public BasketController(ICatalogService catalogService, IBasketService basketService, IOrderService orderService, ILogger<AccountController> logger)
        {
            _catalogService = catalogService;
            _basketService = basketService;
            _orderService = orderService;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            var basket = await _basketService.GetBasket();

            if (basket?.Products == null)
            {
                return View("Error");
            }

            var products = basket.Products.Select( p =>
            {
                var product = _catalogService.GetItemById(p.Product);
                product.Wait();
                return product.Result;
            });


            var data = products.Select(p =>
            {
                var product = p;
                var basketProduct = product.BasketProduct = basket.Products.FirstOrDefault(i => i.Product == p.Id) ?? new BasketProduct { Product = p.Id, Quantity = 0 };
                product.BasketProduct = basketProduct;
                return product;
            });

            return View( new BasketViewModel { Products = data });
        }
    }
}
