using Catalog.Application.Abstractions.Data;
using Catalog.Application.Abstractions.Handlers;
using Catalog.Domain.Entities.Products;
using Microsoft.EntityFrameworkCore;
using Shared.Defaults.Providers;
using Shared.Defaults.Results;

namespace Catalog.Application.Products.Create
{
    public sealed class UpdateByIdProductCommandHandler(
        IApplicationDbContext dbContext,
        IDateTimeProvider dateTimeProvider)
        : ICommandHandler<UpdateByIdProductCommand>
    {
        public async Task<Result> Handle(UpdateByIdProductCommand command, CancellationToken cancellationToken)
        {
            var checkProduct = await dbContext.Products
                .AsNoTracking()
                .Where(p => p.Name == command.Name || p.Id == command.Id)
                .ToListAsync(cancellationToken);

            //Check if product not found
            if (!checkProduct?.Any(x => x.Id == command.Id) ?? true)
            {
                return Result.Failure<Guid>(ProductErrors.NotFound(command.Id));
            }

            //Check for name conflict
            if (checkProduct.Any(x => x.Id != command.Id && x.Name == command.Name))
            {
                return Result.Failure<Guid>(ProductErrors.Conflict(command.Name));
            }

            var product = await dbContext.Products
                    .SingleOrDefaultAsync(x => x.Id == command.Id, cancellationToken);

            product.Name = command.Name;
            product.Price = command.Price;
            product.Quantity = command.Quantity;
            product.UpdatedAt = dateTimeProvider.UtcNow;

            await dbContext.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}