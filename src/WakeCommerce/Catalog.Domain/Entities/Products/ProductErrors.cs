using Shared.Defaults.Results;

namespace Catalog.Domain.Entities.Products
{
    public static class ProductErrors
    {
        public static Error Conflict(string name) => Error.Conflict(
       "Product.Conflict",
       $"The product with the name = '{name}' already exists.");
    }
}