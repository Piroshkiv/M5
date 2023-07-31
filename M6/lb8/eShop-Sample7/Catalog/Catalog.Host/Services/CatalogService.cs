using Catalog.Host.Data;
using Catalog.Host.Models.Dtos;
using Catalog.Host.Models.Enums;
using Catalog.Host.Models.Response;
using Catalog.Host.Repositories.Interfaces;
using Catalog.Host.Services.Interfaces;

namespace Catalog.Host.Services;

public class CatalogService : BaseDataService<ApplicationDbContext>, ICatalogService
{
    private readonly ICatalogItemRepository _catalogItemRepository;
    private readonly ICatalogTypeRepository _catalogTypeRepository;
    private readonly ICatalogBrandRepository _catalogBrandRepository;
    private readonly IMapper _mapper;

    public CatalogService(
        IDbContextWrapper<ApplicationDbContext> dbContextWrapper,
        ILogger<BaseDataService<ApplicationDbContext>> logger,
        ICatalogItemRepository catalogItemRepository,
        ICatalogTypeRepository catalogTypeRepository,
        ICatalogBrandRepository catalogBrandRepository,
        IMapper mapper)
        : base(dbContextWrapper, logger)
    {
        _catalogItemRepository = catalogItemRepository;
        _catalogTypeRepository = catalogTypeRepository;
        _catalogBrandRepository = catalogBrandRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<CatalogItemDto>> GetByBrandAsync(string brand)
    {
        return await ExecuteSafeAsync(async () =>
        {
            var result = await _catalogItemRepository.GetByBrandAsync(brand);
            return result.Select(r => _mapper.Map<CatalogItemDto>(r));
        });
    }

    public async Task<CatalogItemDto?> GetByIdAsync(int id)
    {
        return await ExecuteSafeAsync(async () =>
        {
            var result = await _catalogItemRepository.GetByIdAsync(id);

            if (result == null)
            {
                return null;
            }

            return _mapper.Map<CatalogItemDto>(result);
        });
    }

    public async Task<IEnumerable<CatalogItemDto>> GetByTypeAsync(string type)
    {
        return await ExecuteSafeAsync(async () =>
        {
            var result = await _catalogItemRepository.GetByTypeAsync(type);

            return result.Select(r => _mapper.Map<CatalogItemDto>(r));
        });
    }

    public async Task<PaginatedDataResponse<CatalogItemDto>?> GetCatalogItemsAsync(int pageSize, int pageIndex, Dictionary<CatalogTypeFilter, int>? filters)
    {
        return await ExecuteSafeAsync(async () =>
        {
            int? brandFilter = null;
            int? typeFilter = null;

            if (filters != null)
            {
                if (filters.TryGetValue(CatalogTypeFilter.Brand, out var brand))
                {
                    brandFilter = brand;
                }

                if (filters.TryGetValue(CatalogTypeFilter.Type, out var type))
                {
                    typeFilter = type;
                }
            }

            var result = await _catalogItemRepository.GetByPageAsync(pageIndex, pageSize, brandFilter, typeFilter);
            if (result == null)
            {
                return null;
            }

            return new PaginatedDataResponse<CatalogItemDto>()
            {
                Count = result.TotalCount,
                Data = result.Data.ToList().Select(s => _mapper.Map<CatalogItemDto>(s)).ToList(),
                PageIndex = pageIndex,
                PageSize = pageSize
            };
        });
    }

    public async Task<IEnumerable<CatalogBrandDto>> GetBrandsAsync()
    {
        return await ExecuteSafeAsync(async () =>
        {
            var result = await _catalogBrandRepository.GetBrandsAsync();

            return result.Select(s => _mapper.Map<CatalogBrandDto>(s));
        });
    }

    public async Task<IEnumerable<CatalogTypeDto>> GetTypesAsync()
    {
        return await ExecuteSafeAsync(async () =>
        {
            var result = await _catalogTypeRepository.GetTypesAsync();

            return result.Select(s => _mapper.Map<CatalogTypeDto>(s));
        });
    }
}