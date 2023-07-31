using Catalog.Host.Data.Entities;

namespace Catalog.Host.Repositories.Interfaces;

public interface ICatalogBrandRepository
{
    Task<int?> AddAsync(string brand);
    Task DeleteAsync(int id);
    Task<int?> UpdateAsync(int id, string brand);
    public Task<IEnumerable<CatalogBrand>> GetBrandsAsync();
}