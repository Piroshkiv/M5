using Order.Host.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Order.Host.Data.EntityConfigurations
{
    public class OrderInfoEntityTypeConfiguration
        : IEntityTypeConfiguration<OrderInfo>
    {
        public void Configure(EntityTypeBuilder<OrderInfo> builder)
        {
            builder.ToTable("OrderInfo");

            builder.HasIndex(o => o.Id);

            builder.Property(o => o.SubjectId)
                .IsRequired();

            builder.Property(o => o.Address)
                .IsRequired()
                .HasMaxLength(128);

            builder.Property(o => o.FullName)
                .IsRequired()
                .HasMaxLength(128);

            builder.Property(o => o.Phone)
               .IsRequired()
               .HasMaxLength(20);

            builder.HasMany(o => o.Products)
                .WithOne(o => o.OrderInfo)
                .HasForeignKey(o => o.OrderId);
        }
    }
}
