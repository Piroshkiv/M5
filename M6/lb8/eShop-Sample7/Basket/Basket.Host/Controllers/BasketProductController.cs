using Basket.Host.Models;
using Basket.Host.Services.Interfaces;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Authorization;
using Basket.Host.Models.Response;
using Basket.Host.Models.Request;

namespace Basket.Host.Controllers;

[ApiController]
[Authorize(Policy = AuthPolicy.AllowClientPolicy)]
[Scope("basket.basketproduct")]
[Route(ComponentDefaults.DefaultRoute)]

public class BasketProductController : ControllerBase
{
    private readonly ILogger<BasketProductController> _logger;
    private readonly IBasketService _basketService;

    public BasketProductController(
        ILogger<BasketProductController> logger,
        IBasketService basketService)
    {
        _logger = logger;
        _basketService = basketService;
    }

    [HttpPost]
    [ProducesResponseType(typeof(AddProductResponse), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> Add(AddProductRequest data)
    {
        var basketId = User.Claims.FirstOrDefault(x => x.Type == "sub")?.Value;
        var response = await _basketService.AddAsync(basketId!, data);
        return Ok(response);
    }

    [HttpDelete]
    [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> ClearAll()
    {
        var basketId = User.Claims.FirstOrDefault(x => x.Type == "sub")?.Value;
        var response = await _basketService.ClearAsync(basketId!);
        return Ok(response);
    }

    [HttpDelete]
    [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> Remove(int id)
    {
        var basketId = User.Claims.FirstOrDefault(x => x.Type == "sub")?.Value;
        var response = await _basketService.RemoveProductAsync(basketId!, id);
        return Ok(response);
    }

    [HttpPost]
    [ProducesResponseType(typeof(ProductsResponse), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> Increment(int id)
    {
        var basketId = User.Claims.FirstOrDefault(x => x.Type == "sub")?.Value;
        var response = await _basketService.IncrementProductAsync(basketId!, id);
        return Ok(response);
    }

    [HttpPost]
    [ProducesResponseType(typeof(ProductsResponse), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> Decrement(int id)
    {
        var basketId = User.Claims.FirstOrDefault(x => x.Type == "sub")?.Value;
        var response = await _basketService.DecrementProductAsync(basketId!, id);
        return Ok(response);
    }
}