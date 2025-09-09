using Catalog.Application.Abstractions.Requests;

namespace Catalog.Application.Products.GetById
{
    public sealed class GetByIdProductQuery : IQuery<GetByIdProductResponse>
    {
        public GetByIdProductQuery(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }
    }
}