using Catalog.Host.Data;
using Catalog.Host.Data.Entities;

namespace Catalog.Host.Repositories.Interfaces;

public interface ICatalogBrandRepository
{
    Task<PaginatedData<CatalogBrand>> GetByPageAsync(int pageIndex, int pageSize);
    Task<int?> AddAsync(string brand);
    Task DeleteAsync(int id);
    Task<int?> UpdateAsync(int id, string brand);
}