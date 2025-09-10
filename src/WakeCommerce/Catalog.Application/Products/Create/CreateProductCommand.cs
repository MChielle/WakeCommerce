using Catalog.Application.Abstractions.Requests;

namespace Catalog.Application.Products.Create
{
    public sealed class CreateProductCommand : ICommand<Guid>
    {
        public string Name { get; set; }
        public decimal Quantity { get; set; }
        public decimal Price { get; set; }
    }
}