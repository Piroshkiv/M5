using Microsoft.Extensions.Logging;
using MVC.Services.Interfaces;
using MVC.ViewModels.CatalogViewModels;
using MVC.ViewModels.Basket;
using MVC.ViewModels.Pagination;
using MVC.ViewModels;

namespace MVC.Controllers;

public class CatalogController : Controller
{
    private readonly ICatalogService _catalogService;
    private readonly IBasketService _basketService;
    private readonly ILogger<AccountController> _logger;

    public CatalogController(ICatalogService catalogService, IBasketService basketService, ILogger<AccountController> logger)
    {
        _catalogService = catalogService;
        _basketService = basketService;
        _logger = logger;
    }

    public async Task<IActionResult> Index(int? brandFilterApplied, int? typesFilterApplied, int? page, int? itemsPage)
    {   
        page ??= 0;
        itemsPage ??= 6;
     
        var catalog = await _catalogService.GetCatalogItems(page.Value, itemsPage.Value, brandFilterApplied, typesFilterApplied);

        if (User.Identity.IsAuthenticated)
        {
            var basket = await _basketService.GetBasket();
            var data = catalog.Data.Select(p =>
            {
                var product = p;
                var basketProduct = p.BasketProduct = basket.Products.FirstOrDefault(i => i.Product == p.Id) ?? new BasketProduct { Product = p.Id, Quantity = 0 };
                product.BasketProduct = basketProduct;
                return product;
            });

            catalog = new Catalog { Count = catalog.Count, PageIndex = catalog.PageIndex, PageSize = catalog.PageSize, Data = data.ToList() };
        }

       
        if (catalog == null)
        {
            return View("Error");
        }
        var info = new PaginationInfo()
        {
            ActualPage = page.Value,
            ItemsPerPage = catalog.Data.Count,
            TotalItems = catalog.Count,
            TotalPages = (int)Math.Ceiling((decimal)catalog.Count / itemsPage.Value)
        };
        var vm = new IndexViewModel()
        {
            CatalogItems = catalog.Data,
            Brands = await _catalogService.GetBrands(),
            Types = await _catalogService.GetTypes(),
            PaginationInfo = info
        };

        vm.PaginationInfo.Next = (vm.PaginationInfo.ActualPage == vm.PaginationInfo.TotalPages - 1) ? "is-disabled" : "";
        vm.PaginationInfo.Previous = (vm.PaginationInfo.ActualPage == 0) ? "is-disabled" : "";



        return View(vm);
    }

    [HttpGet]
    public async Task<IActionResult> AddToBasket(int id)
    {
        if (!User.Identity.IsAuthenticated)
        {
            RedirectToAction(nameof(AccountController.SignIn), "Account");
        }

        var p = await _basketService.Add(id);


        return RedirectToAction(nameof(CatalogController.Index), "Catalog");
    }
    [HttpGet]
    public async Task<IActionResult> Increment(int id)
    {
        if (!User.Identity.IsAuthenticated)
        {
            RedirectToAction(nameof(AccountController.SignIn), "Account");
        }

        var p = await _basketService.Increment(id);

        return RedirectToAction(nameof(CatalogController.Index), "Catalog");
    }
    [HttpGet]
    public async Task<IActionResult> Decrement(int id)
    {
        if (!User.Identity.IsAuthenticated)
        {
            RedirectToAction(nameof(AccountController.SignIn), "Account");
        }

        var p = await _basketService.Decrement(id);

        return RedirectToAction(nameof(CatalogController.Index), "Catalog");
    }
    [HttpGet]
    public async Task<IActionResult> Remove(int id)
    {
        if (!User.Identity.IsAuthenticated)
        {
            RedirectToAction(nameof(AccountController.SignIn), "Account");
        }

        var p = await _basketService.Remove(id);

        return RedirectToAction(nameof(CatalogController.Index), "Catalog");
    }
}