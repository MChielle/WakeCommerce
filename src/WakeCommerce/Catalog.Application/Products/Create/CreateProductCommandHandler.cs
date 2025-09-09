using Catalog.Application.Abstractions.Data;
using Catalog.Application.Abstractions.Handlers;
using Catalog.Domain.Entities.Products;
using Microsoft.EntityFrameworkCore;
using Shared.Defaults.Providers;
using Shared.Defaults.Results;

namespace Catalog.Application.Products.Create
{
    public sealed class CreateProductCommandHandler(
        IApplicationDbContext dbContext,
        IDateTimeProvider dateTimeProvider) 
        : ICommandHandler<CreateProductCommand, Guid>
    {
        public async Task<Result<Guid>> Handle(CreateProductCommand command, CancellationToken cancellationToken)
        {
            var product = await dbContext.Products
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Name == command.Name, cancellationToken);

            if (product is not null)
            {
                return Result.Failure<Guid>(ProductErrors.Conflict(command.Name));
            }

            var productToAdd = new Product(command.Name, command.Quantity, command.Price, dateTimeProvider.UtcNow);

            await dbContext.Products.AddAsync(productToAdd, cancellationToken);

            await dbContext.SaveChangesAsync(cancellationToken);

            return productToAdd.Id;
        }
    }
}