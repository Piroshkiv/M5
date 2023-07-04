using Catalog.Host.Models.Dtos;
using Catalog.Host.Models.Enums;
using Catalog.Host.Models.Response;

namespace Catalog.Host.Services.Interfaces;

public interface ICatalogService
{
    Task<PaginatedDataResponse<CatalogItemDto>?> GetCatalogItemsAsync(int pageSize, int pageIndex, Dictionary<CatalogTypeFilter, int>? filters);
    Task<CatalogItemDto?> GetByIdAsync(int id);
    Task<IEnumerable<CatalogItemDto>> GetByBrandAsync(string brand);
    Task<IEnumerable<CatalogItemDto>> GetByTypeAsync(string type);
    public Task<IEnumerable<CatalogBrandDto>> GetBrandsAsync();
    public Task<IEnumerable<CatalogTypeDto>> GetTypesAsync();
}