using Catalog.Domain.Entities.Products;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Application.Abstractions.Data
{
    public interface IApplicationDbContext
    {
        DbSet<Product> Products { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}