using Catalog.Application.Abstractions.Data;
using Catalog.Application.Abstractions.Handlers;
using Catalog.Domain.Entities.Products;
using Microsoft.EntityFrameworkCore;
using Shared.Defaults.Results;
using System.Linq.Expressions;

namespace Catalog.Application.Products.GetByFilter
{
    public class GetByFilterProductQueryHandler(IApplicationDbContext context)
        : IQueryHandler<GetByFilterProductQuery, GetByFilterProductResponse>
    {
        public async Task<Result<GetByFilterProductResponse>> Handle(GetByFilterProductQuery request, CancellationToken cancellationToken)
        {
            var query = context.Products.AsQueryable();

            if (!string.IsNullOrWhiteSpace(request.SearchTerm))
            {
                query = query.Where(x => x.Name.Contains(request.SearchTerm));
            }

            if (request.SortOrder?.ToLower() == "desc")
            {
                query = query.OrderByDescending(GetSortColumn(request));
            }
            else
            {
                query = query.OrderBy(GetSortColumn(request));
            }

            if (request.Page.HasValue && request.PageSize.HasValue)
                query = query.Skip(request.Page.Value * request.PageSize.Value)
                             .Take(request.PageSize.Value);

            var products = await query
                .Select(entity => new FilteredAndOrderedProductResponse
                {
                    Id = entity.Id,
                    Name = entity.Name,
                    Price = entity.Price,
                    Quantity = entity.Quantity
                }).ToListAsync(cancellationToken);

            if (products.Count == 0)
            {
                return Result.Failure<GetByFilterProductResponse>(ProductErrors.NotFound());
            }

            var response = new GetByFilterProductResponse
            {
                Products = products
            };

            return response;
        }

        private static Expression<Func<Product, object>> GetSortColumn(GetByFilterProductQuery request) =>
            request.SortColumn?.ToLower() switch
            {
                "name" => product => product.Name,
                "quantity" => product => product.Quantity,
                "price" => product => product.Price,
                _ => product => product.Id
            };
    }
}