using Catalog.Host.Data;
using Catalog.Host.Data.Entities;
using Catalog.Host.Repositories.Interfaces;
using Catalog.Host.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Host.Repositories;

public class CatalogTypeRepository : ICatalogTypeRepository
{
    private readonly ApplicationDbContext _dbContext;
    private readonly ILogger<CatalogItemRepository> _logger;

    public CatalogTypeRepository(
        IDbContextWrapper<ApplicationDbContext> dbContextWrapper,
        ILogger<CatalogItemRepository> logger)
    {
        _dbContext = dbContextWrapper.DbContext;
        _logger = logger;
    }

    public async Task<PaginatedData<CatalogType>> GetByPageAsync(int pageIndex, int pageSize)
    {
        var totalIBrands = await _dbContext.CatalogTypes.LongCountAsync();

        var itemsOnPage = await _dbContext.CatalogTypes
            .OrderBy(c => c.Type)
            .Skip(pageSize * pageIndex)
            .Take(pageSize)
            .ToListAsync();

        return new PaginatedData<CatalogType>() { TotalCount = totalIBrands, Data = itemsOnPage };
    }

    public async Task<int?> AddAsync(string type)
    {
        var item = await _dbContext.AddAsync(new CatalogType
        {
            Type = type,
        });

        await _dbContext.SaveChangesAsync();

        return item.Entity.Id;
    }

    public async Task DeleteAsync(int id)
    {
        _dbContext.Remove(new CatalogType { Id = id });
        await _dbContext.SaveChangesAsync();
    }

    public async Task<int?> UpdateAsync(int id, string type)
    {
        var item = _dbContext.Update(new CatalogType
        {
            Id = id,
            Type = type
        });

        await _dbContext.SaveChangesAsync();

        return item.Entity.Id;
    }

    public async Task<IEnumerable<CatalogType>> GetTypesAsync()
    {
        var items = await _dbContext.CatalogTypes
            .OrderBy(c => c.Type)
            .ToListAsync();

        return items;
    }
}