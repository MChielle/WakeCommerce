using Catalog.Domain.Entities.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Catalog.Infrastructure.Database.Seeders
{
    internal class SeedProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasData(new List<Product>
            {
                new Product(Guid.Parse("00000000-0000-0000-0000-000000000001"),"Produto 1", 10, 1.50M, SeedConstants.DEFAULT_SEED_DATETIME, null),
                new Product(Guid.Parse("00000000-0000-0000-0000-000000000002"),"Produto 2", 3.333M, 1.50M,SeedConstants.DEFAULT_SEED_DATETIME, null),
                new Product(Guid.Parse("00000000-0000-0000-0000-000000000003"),"Produto 3", 4.10M, 1.99M, SeedConstants.DEFAULT_SEED_DATETIME, null),
                new Product(Guid.Parse("00000000-0000-0000-0000-000000000004"),"Produto 4", 7.10M, 1.50M, SeedConstants.DEFAULT_SEED_DATETIME, null),
                new Product(Guid.Parse("00000000-0000-0000-0000-000000000005"),"Produto 5", 3.33M, 1.50M, SeedConstants.DEFAULT_SEED_DATETIME, null)
            });
        }
    }
}