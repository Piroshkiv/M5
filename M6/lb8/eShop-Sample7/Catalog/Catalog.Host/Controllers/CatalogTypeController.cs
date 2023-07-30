using Catalog.Host.Models.Requests;
using Catalog.Host.Models.Response;
using Catalog.Host.Services.Interfaces;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Authorization;

namespace Catalog.Host.Controllers;

[ApiController]
[Authorize(Policy = AuthPolicy.AllowClientPolicy)]
[Scope("catalog.catalogtype")]
[Route(ComponentDefaults.DefaultRoute)]
public class CatalogTypeController : ControllerBase
{
    private readonly ILogger<CatalogTypeController> _logger;
    private readonly ICatalogTypeService _catalogTypeService;

    public CatalogTypeController(
        ILogger<CatalogTypeController> logger,
        ICatalogTypeService catalogTypeService)
    {
        _logger = logger;
        _catalogTypeService = catalogTypeService;
    }

    [HttpPut]
    [ProducesResponseType(typeof(AddDataResponse<int?>), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> Add(CreateTypeRequest request)
    {
        var result = await _catalogTypeService.AddAsync(request.Type);
        return Ok(new AddDataResponse<int?>() { Id = result });
    }

    [HttpDelete]
    public async Task<IActionResult> Delete(DeleteDataRequest request)
    {
        await _catalogTypeService.DeleteAsync(request.Id);
        return new EmptyResult();
    }

    [HttpPost]
    [ProducesResponseType(typeof(UpdateDataResponse<int?>), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> Update(UpdateTypeRequest request)
    {
        var result = await _catalogTypeService.UpdateAsync(request.Id, request.Type);
        return Ok(new UpdateDataResponse<int?>() { Id = result });
    }
}