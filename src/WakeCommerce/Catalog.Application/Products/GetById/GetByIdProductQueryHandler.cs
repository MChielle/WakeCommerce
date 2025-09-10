using Catalog.Application.Abstractions.Data;
using Catalog.Application.Abstractions.Handlers;
using Catalog.Domain.Entities.Products;
using Microsoft.EntityFrameworkCore;
using Shared.Defaults.Results;

namespace Catalog.Application.Products.GetById
{
    public class GetByIdProductQueryHandler(IApplicationDbContext context)
        : IQueryHandler<GetByIdProductQuery, GetByIdProductResponse>
    {
        public async Task<Result<GetByIdProductResponse>> Handle(GetByIdProductQuery query, CancellationToken cancellationToken)
        {
            var product = await context.Products
                .Where(x => x.Id.Equals(query.Id))
                .Select(entity => new GetByIdProductResponse
                {
                    Name = entity.Name,
                    Price = entity.Price,
                    Quantity = entity.Quantity,
                    UpdatedAt = entity.UpdatedAt,
                    CreatedAt = entity.CreatedAt
                })
                .SingleOrDefaultAsync(cancellationToken);

            if (product is null)
            {
                return Result.Failure<GetByIdProductResponse>(ProductErrors.NotFound(query.Id));
            }

            return product;
        }
    }
}