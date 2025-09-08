using System.ComponentModel.DataAnnotations.Schema;

namespace Catalog.Domain.Entities.Products
{
    public sealed class Product : Entity
    {
        public Product(string name, decimal quantity, decimal price)
        {
            Name = name;
            Quantity = quantity;
            Price = price;
        }

        public Product(Guid id, string name, decimal quantity, decimal price)
            : base(id)
        {
            Name = name;
            Quantity = quantity;
            Price = price;
        }

        private Product() //Required by EF
        {
        }

        public string Name { get; set; }

        public decimal Quantity { get; set; }

        public decimal Price { get; set; }

        #region Entity Definitions

        [NotMapped]
        public const int NameMaxLength = 100;

        [NotMapped]
        public static readonly (int min, decimal max, int precision, int scale) QuantityPattern = new(0, 99999.999M, 8, 3);

        [NotMapped]
        public static readonly (int min, decimal max, int precision, int scale) PricePattern = new(0, 999999.99M, 8, 2);

        #endregion Entity Definitions
    }
}