namespace Catalog.Application.Products.GetById
{
    public sealed class GetByIdProductResponse
    {
        public string Name { get; set; }

        public decimal Quantity { get; set; }

        public decimal Price { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }
    }
}