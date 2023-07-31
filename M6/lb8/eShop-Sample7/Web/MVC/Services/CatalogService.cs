using Infrastructure.Services.Interfaces;
using MVC.Dtos;
using MVC.Models.Enums;
using MVC.Models.Requests;
using MVC.Models.Response;
using MVC.Services.Interfaces;
using MVC.ViewModels;

namespace MVC.Services;

public class CatalogService : ICatalogService
{
    private readonly IOptions<AppSettings> _settings;
    private readonly IHttpClientService _httpClient;
    private readonly ILogger<CatalogService> _logger;

    public CatalogService(IHttpClientService httpClient, ILogger<CatalogService> logger, IOptions<AppSettings> settings)
    {
        _httpClient = httpClient;
        _settings = settings;
        _logger = logger;
    }

    public async Task<Catalog> GetCatalogItems(int page, int take, int? brand, int? type)
    {
        var filters = new Dictionary<CatalogTypeFilter, int>();

        if (brand.HasValue)
        {
            filters.Add(CatalogTypeFilter.Brand, brand.Value);
        }
        
        if (type.HasValue)
        {
            filters.Add(CatalogTypeFilter.Type, type.Value);
        }
        
        var result = await _httpClient.SendAsync<Catalog, PaginatedItemsRequest<CatalogTypeFilter>>($"{_settings.Value.CatalogUrl}/items",
           HttpMethod.Post, 
           new PaginatedItemsRequest<CatalogTypeFilter>()
            {
                PageIndex = page,
                PageSize = take,
                Filters = filters
            });

        return result;
    }

    public async Task<IEnumerable<SelectListItem>> GetBrands()
    {
        var result = await _httpClient.SendAsync<IEnumerable<CatalogBrand>, object?>($"{_settings.Value.CatalogUrl}/brands",
                   HttpMethod.Post,
                   null);

        return result.Select(r => new SelectListItem() { Value = r.Id.ToString(), Text = r.Brand });
    }

    public async Task<IEnumerable<SelectListItem>> GetTypes()
    {
        var result = await _httpClient.SendAsync<IEnumerable<CatalogType>, object?>($"{_settings.Value.CatalogUrl}/types",
                   HttpMethod.Post,
                   null);

        return result.Select(r => new SelectListItem() { Value = r.Id.ToString(), Text = r.Type });
    }

    public async Task<CatalogItem> GetItemById(int id)
    {
        var result = await _httpClient.SendAsync<ItemResponse, DataRequest<int>>($"{_settings.Value.CatalogUrl}/itemById",
                   HttpMethod.Get,
                   new DataRequest<int> { Value = id});

        return new CatalogItem { Id = result.Id, PictureUrl = result.PictureUrl, Name = result.Name, AvailableStock = result.AvailableStock, Description = result.Description, Price = result.Price };
    }
}
