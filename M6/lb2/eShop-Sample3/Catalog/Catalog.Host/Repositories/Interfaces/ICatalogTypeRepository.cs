using Catalog.Host.Data.Entities;
using Catalog.Host.Data;
namespace Catalog.Host.Repositories.Interfaces;

public interface ICatalogTypeRepository
{
    Task<PaginatedData<CatalogType>> GetByPageAsync(int pageIndex, int pageSize);
    Task<IEnumerable<CatalogType>> GetTypesAsync();
    Task<int?> AddAsync(string type);
    Task DeleteAsync(int id);
    Task<int?> UpdateAsync(int id, string type);
}