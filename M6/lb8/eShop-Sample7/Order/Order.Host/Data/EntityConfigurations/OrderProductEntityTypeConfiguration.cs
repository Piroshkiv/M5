using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Order.Host.Data.Entities;

namespace Order.Host.Data.EntityConfigurations
{
    public class OrderProductEntityTypeConfiguration
        : IEntityTypeConfiguration<OrderProduct>
    {
        public void Configure(EntityTypeBuilder<OrderProduct> builder)
        {
            builder.ToTable("OrderProduct");

            builder.HasIndex(p => p.Id);

            builder.Property(p => p.OrderId)
                .IsRequired();

            builder.Property(p => p.ProductId)
                .IsRequired();

            builder.Property(p => p.Quantity)
               .IsRequired();

            builder.HasOne(p => p.OrderInfo)
                .WithMany(o => o.Products)
                .HasForeignKey(p => p.ProductId);
        }
    }
}
