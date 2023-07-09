using Basket.Host.Models;
using Basket.Host.Services.Interfaces;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Authorization;
using Basket.Host.Models.Response;
using Basket.Host.Models.Request;

namespace Basket.Host.Controllers;

[ApiController]
[Authorize(Policy = AuthPolicy.AllowEndUserPolicy)]
[Route(ComponentDefaults.DefaultRoute)]
public class BasketBffController : ControllerBase
{
    private readonly ILogger<BasketBffController> _logger;
    private readonly IBasketService _basketService;

    public BasketBffController(
        ILogger<BasketBffController> logger,
        IBasketService basketService)
    {
        _logger = logger;
        _basketService = basketService;
    }

    [HttpPost]
    [ProducesResponseType(typeof(AddProductResponse), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> Log(AddProductRequest data)
    {
        var basketId = User.Claims.FirstOrDefault(x => x.Type == "sub")?.Value;
        await _basketService.Log(basketId??"Anonym");
        return Ok();
    }

    [HttpGet]
    [ProducesResponseType(typeof(ProductsResponse), (int)HttpStatusCode.OK)]
    [RateLimitFilter(60, 10)]
    public async Task<IActionResult> Basket()
    {
        var basketId = User.Claims.FirstOrDefault(x => x.Type == "sub")?.Value;
        var response = await _basketService.GetAsync(basketId!);
        return Ok(response);
    }

    [HttpGet]
    [ProducesResponseType(typeof(ProductsResponse), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> BasketById(int id)
    {
        var basketId = User.Claims.FirstOrDefault(x => x.Type == "sub")?.Value;
        var response = await _basketService.GetProductByIdAsync(basketId!, id);
        return Ok(response);
    }

}