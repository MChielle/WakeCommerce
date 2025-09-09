using Catalog.Application.Abstractions.Requests;
using System.Text.Json.Serialization;

namespace Catalog.Application.Products.Create
{
    public sealed class UpdateByIdProductCommand : ICommand
    {
        [JsonIgnore]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public decimal Quantity { get; set; }
        public decimal Price { get; set; }
    }
}