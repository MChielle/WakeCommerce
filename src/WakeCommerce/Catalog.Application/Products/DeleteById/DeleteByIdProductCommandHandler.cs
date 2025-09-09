using Catalog.Application.Abstractions.Data;
using Catalog.Application.Abstractions.Handlers;
using Catalog.Application.Products.GetById;
using Catalog.Domain.Entities.Products;
using Microsoft.EntityFrameworkCore;
using Shared.Defaults.Providers;
using Shared.Defaults.Results;

namespace Catalog.Application.Products.DeleteById
{
    public class DeleteByIdProductCommandHandler
        (IApplicationDbContext dbContext,
        IDateTimeProvider dateTimeProvider)
        : ICommandHandler<DeleteByIdProductCommand>
    {
        public async Task<Result> Handle(DeleteByIdProductCommand command, CancellationToken cancellationToken)
        {
            var product = await dbContext.Products
                    .SingleOrDefaultAsync(x => x.Id == command.Id, cancellationToken);

            //Check if product not found
            if (product is null)
            {
                return Result.Failure<Guid>(ProductErrors.NotFound(command.Id));
            }

            dbContext.Products.Remove(product);

            await dbContext.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}