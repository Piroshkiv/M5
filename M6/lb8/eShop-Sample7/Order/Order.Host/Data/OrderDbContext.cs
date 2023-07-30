using Order.Host.Data.Entities;
using Order.Host.Data.EntityConfigurations;

namespace Order.Host.Data
{
    public class OrderDbContext : DbContext
    {
        public OrderDbContext(DbContextOptions<OrderDbContext> options)
            : base(options)
        {
        }

        public DbSet<OrderInfo> OrdersInfo { get; set; } = null!;
        public DbSet<OrderProduct> OrderProducts { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new OrderInfoEntityTypeConfiguration());
            builder.ApplyConfiguration(new OrderProductEntityTypeConfiguration());
        }
    }
}