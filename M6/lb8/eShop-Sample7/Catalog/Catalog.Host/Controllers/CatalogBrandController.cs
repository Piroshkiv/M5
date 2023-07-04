using Catalog.Host.Models.Requests;
using Catalog.Host.Models.Response;
using Catalog.Host.Services.Interfaces;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Authorization;

namespace Catalog.Host.Controllers;

[ApiController]
[Authorize(Policy = AuthPolicy.AllowClientPolicy)]
[Scope("catalog.catalogbrand")]
[Route(ComponentDefaults.DefaultRoute)]
public class CatalogBrandController : ControllerBase
{
    private readonly ILogger<CatalogBrandController> _logger;
    private readonly ICatalogBrandService _catalogBrandService;

    public CatalogBrandController(
        ILogger<CatalogBrandController> logger,
        ICatalogBrandService catalogBrandService)
    {
        _logger = logger;
        _catalogBrandService = catalogBrandService;
    }

    [HttpPost]
    [ProducesResponseType(typeof(AddDataResponse<int?>), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> Add(CreateBrandRequest request)
    {
        var result = await _catalogBrandService.AddAsync(request.Brand);
        return Ok(new AddDataResponse<int?>() { Id = result });
    }

    [HttpPost]
    public async Task<IActionResult> Delete(DeleteDataRequest request)
    {
        await _catalogBrandService.DeleteAsync(request.Id);
        return new EmptyResult();
    }

    [HttpPost]
    [ProducesResponseType(typeof(UpdateDataResponse<int?>), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> Update(UpdateBrandRequest request)
    {
        var result = await _catalogBrandService.UpdateAsync(request.Id, request.Brand);
        return Ok(new UpdateDataResponse<int?>() { Id = result });
    }
}