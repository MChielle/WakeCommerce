using Catalog.Application.Abstractions.Requests;
using Catalog.Application.Products.GetByFilterOrdered;

namespace Catalog.Application.Products.GetByFilter
{
    public class GetByFilterProductQuery : IQuery<GetByFilterProductResponse>
    {
        public string? SearchTerm { get; set; }
        public string? SortColumn { get; set; }
        public string? SortOrder { get; set; }
        public int? Page { get; set; }
        public int? PageSize { get; set; }
    }
}