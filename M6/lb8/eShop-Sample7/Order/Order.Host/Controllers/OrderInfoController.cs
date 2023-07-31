using Infrastructure.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Order.Host.Models.Dtos;
using Order.Host.Models.Request;
using Order.Host.Models.Response;
using Order.Host.Services.Interfaces;
using System.Net;

namespace Order.Host.Controllers
{
    [ApiController]
    [Authorize(Policy = AuthPolicy.AllowClientPolicy)]
    [Scope("order.orderinfo")]
    [Route(ComponentDefaults.DefaultRoute)]
    public class OrderInfoController : ControllerBase
    {
        private readonly ILogger<OrderInfoController> _logger;
        private readonly IOrderInfoService _orderInfoService;

        public OrderInfoController(
            ILogger<OrderInfoController> logger,
            IOrderInfoService orderInfoService)
        {
            _logger = logger;
            _orderInfoService = orderInfoService;
        }

        [HttpPost]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Add(OrderRequest order)
        {
            var userId = User.Claims.FirstOrDefault(x => x.Type == "sub")?.Value;
            var response = await _orderInfoService.AddAsync( new OrderInfoDto() { SubjectId = int.Parse(userId), FullName = order.FullName, Address = order.FullName, Phone = order.Phone} );
            return Ok(response);
        }

        [HttpPatch]
        [ProducesResponseType(typeof(GetDataResponse<OrderInfoDto>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> SetFormed(int orderId)
        {
            var userId = User.Claims.FirstOrDefault(x => x.Type == "sub")?.Value;
            var response =  new GetDataResponse<OrderInfoDto> { Data = await _orderInfoService.FormAsync(orderId) };
            return Ok(response);
        }

        [HttpPatch]
        [ProducesResponseType(typeof(GetDataResponse<OrderInfoDto>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> SetSended(int orderId)
        {
            var userId = User.Claims.FirstOrDefault(x => x.Type == "sub")?.Value;
            var response = new GetDataResponse<OrderInfoDto> { Data = await _orderInfoService.SendAsync(orderId) };
            return Ok(response);
        }

        [HttpPatch]
        [ProducesResponseType(typeof(GetDataResponse<OrderInfoDto>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> SetTaken(int orderId)
        {
            var userId = User.Claims.FirstOrDefault(x => x.Type == "sub")?.Value;
            var response = new GetDataResponse<OrderInfoDto> { Data = await _orderInfoService.TakeAsync(orderId) };
            return Ok(response);
        }

    }
}
