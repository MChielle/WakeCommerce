using Shared.Defaults.Results;

namespace Catalog.Domain.Entities.Products
{
    public static class ProductErrors
    {
        public static Error Conflict(string name) => Error.Conflict(
       "Product.Conflict",
       $"The product with the name = '{name}' already exists.");

        public static Error NotFound(Guid id) => Error.NotFound(
        "Product.NotFound",
        $"The product with the id = '{id}' not found.");

        public static Error NotFound() => Error.NotFound(
        "Product.NotFound",
        $"No product was not found.");
    }
}