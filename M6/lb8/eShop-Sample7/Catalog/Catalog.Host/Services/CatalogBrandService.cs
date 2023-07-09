using Catalog.Host.Data;
using Catalog.Host.Repositories.Interfaces;
using Catalog.Host.Services.Interfaces;

namespace Catalog.Host.Services;

public class CatalogBrandService : BaseDataService<ApplicationDbContext>, ICatalogBrandService
{
    private readonly ICatalogBrandRepository _catalogBrandRepository;

    public CatalogBrandService(
        IDbContextWrapper<ApplicationDbContext> dbContextWrapper,
        ILogger<BaseDataService<ApplicationDbContext>> logger,
        ICatalogBrandRepository catalogBrandRepository)
        : base(dbContextWrapper, logger)
    {
        _catalogBrandRepository = catalogBrandRepository;
    }

    public Task<int?> AddAsync(string brand)
    {
        return ExecuteSafeAsync(() => _catalogBrandRepository.AddAsync(brand));
    }

    public Task DeleteAsync(int id)
    {
        return ExecuteSafeAsync(() => _catalogBrandRepository.DeleteAsync(id));
    }

    public Task<int?> UpdateAsync(int id, string brand)
    {
        return ExecuteSafeAsync(() => _catalogBrandRepository.UpdateAsync(id, brand));
    }
}