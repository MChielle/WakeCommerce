using Catalog.Domain.Entities.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Catalog.Infrastructure.EntitiesConfiguration
{
    internal sealed class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(t => t.Id);

            builder.Property(t => t.CreatedAt)
                .HasConversion(d => d != null ? DateTime.SpecifyKind(d.Date, DateTimeKind.Utc) : d, v => v);

            builder.Property(t => t.UpdatedAt)
                .HasConversion(d => d != null ? DateTime.SpecifyKind(d.Date, DateTimeKind.Utc) : d, v => v);

            builder.Property(t => t.Quantity)
                .HasPrecision(Product.QuantityPattern.precision, Product.QuantityPattern.scale);

            builder.Property(t => t.Price)
                .HasPrecision(Product.PricePattern.precision, Product.PricePattern.scale);
        }
    }
}