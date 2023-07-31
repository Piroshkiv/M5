using Microsoft.AspNetCore.Mvc;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Authorization;
using System.Net;
using Order.Host.Services.Interfaces;
using Order.Host.Models.Response;
using Order.Host.Models.Dtos;

namespace Order.Host.Controllers
{
    [ApiController]
    [Authorize(Policy = AuthPolicy.AllowEndUserPolicy)]
    [Route(ComponentDefaults.DefaultRoute)]
    public class OrderBffController: ControllerBase
    {
        private readonly ILogger<OrderBffController> _logger;
        private readonly IOrderInfoService _orderInfoService;

        public OrderBffController(
            ILogger<OrderBffController> logger,
            IOrderInfoService orderInfoService)
        {
            _logger = logger;
            _orderInfoService = orderInfoService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(GetDataResponse<OrderInfoDto>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> OrderById(int id)
        {
            var response = new GetDataResponse<OrderInfoDto> { Data = await _orderInfoService.GetAsync(id) };
            return Ok(response);
        }

        [HttpGet]
        [ProducesResponseType(typeof(GetDataResponse<IEnumerable<OrderInfoDto>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Orders()
        {
            var userId = User.Claims.FirstOrDefault(x => x.Type == "sub")?.Value;
            var response = new GetDataResponse<IEnumerable<OrderInfoDto>> { Data = await _orderInfoService.GetByUserAsync( int.Parse(userId) ) };
            return Ok(response);
        }
    }
}
