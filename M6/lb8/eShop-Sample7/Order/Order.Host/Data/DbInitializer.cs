using Order.Host.Data.Entities;

namespace Order.Host.Data;

public static class DbInitializer
{
    public static async Task Initialize(OrderDbContext context)
    {
        await context.Database.EnsureCreatedAsync();
    }
}