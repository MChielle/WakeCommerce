namespace Catalog.Application.Products.GetByFilter
{
    public class GetByFilterProductResponse
    {
        public IEnumerable<FilteredAndOrderedProductResponse> Products { get; set; }
    }

    public class FilteredAndOrderedProductResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public decimal Quantity { get; set; }
        public decimal Price { get; set; }
    }
}