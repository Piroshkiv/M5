using Infrastructure.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Order.Host.Models.Dtos;
using Order.Host.Models.Request;
using Order.Host.Services.Interfaces;
using System.Net;

namespace Order.Host.Controllers
{
    [ApiController]
    [Authorize(Policy = AuthPolicy.AllowClientPolicy)]
    [Scope("order.orderproduct")]
    [Route(ComponentDefaults.DefaultRoute)]
    public class OrderProductController : ControllerBase
    {
        private readonly ILogger<OrderProductController> _logger;
        private readonly IOrderProductService _orderProductService;

        public OrderProductController(
            ILogger<OrderProductController> logger,
            IOrderProductService orderProductService)
        {
            _logger = logger;
            _orderProductService = orderProductService;
        }

        [HttpPost]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Add(ProductRequest product)
        {
            var userId = User.Claims.FirstOrDefault(x => x.Type == "sub")?.Value;
            var response = await _orderProductService.AddAsync( new OrderProductDto() { Order = product.Order, Product = product.Product, Quantity = product.Quentity });
            return Ok(response);
        }
    }
}