using Catalog.Host.Data;
using Catalog.Host.Data.Entities;
using Catalog.Host.Repositories.Interfaces;
using Catalog.Host.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Host.Repositories;

public class CatalogBrandRepository : ICatalogBrandRepository
{
    private readonly ApplicationDbContext _dbContext;
    private readonly ILogger<CatalogItemRepository> _logger;

    public CatalogBrandRepository(
        IDbContextWrapper<ApplicationDbContext> dbContextWrapper,
        ILogger<CatalogItemRepository> logger)
    {
        _dbContext = dbContextWrapper.DbContext;
        _logger = logger;
    }

    public async Task<int?> AddAsync(string brand)
    {
        var item = await _dbContext.AddAsync(new CatalogBrand
        {
            Brand = brand,
        });

        await _dbContext.SaveChangesAsync();

        return item.Entity.Id;
    }

    public async Task DeleteAsync(int id)
    {
        _dbContext.Remove(new CatalogBrand { Id = id });
        await _dbContext.SaveChangesAsync();
    }

    public async Task<int?> UpdateAsync(int id, string brand)
    {
        var item = _dbContext.Update(new CatalogBrand
        {
            Id = id,
            Brand = brand
        });

        await _dbContext.SaveChangesAsync();

        return item.Entity.Id;
    }

    public async Task<IEnumerable<CatalogBrand>> GetBrandsAsync()
    {
        var items = await _dbContext.CatalogBrands
            .OrderBy(c => c.Brand)
            .ToListAsync();

        return items;
    }
}