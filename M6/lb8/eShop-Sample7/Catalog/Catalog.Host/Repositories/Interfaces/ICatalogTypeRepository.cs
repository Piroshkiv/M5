using Catalog.Host.Data.Entities;

namespace Catalog.Host.Repositories.Interfaces;

public interface ICatalogTypeRepository
{
    Task<IEnumerable<CatalogType>> GetTypesAsync();
    Task<int?> AddAsync(string type);
    Task DeleteAsync(int id);
    Task<int?> UpdateAsync(int id, string type);
}