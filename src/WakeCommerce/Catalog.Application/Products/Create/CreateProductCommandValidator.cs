using Catalog.Domain.Entities.Products;
using FluentValidation;

namespace Catalog.Application.Products.Create
{
    public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
    {
        public CreateProductCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Product name is required.")
                .MaximumLength(Product.NameMaxLength).WithMessage($"Product name must not exceed {Product.NameMaxLength} characters.");

            RuleFor(x => x.Quantity)
                .InclusiveBetween(Product.QuantityPattern.min, Product.QuantityPattern.max).WithMessage($"Quantity must be between {Product.QuantityPattern.min} and {Product.QuantityPattern.max}, with {Product.QuantityPattern.scale} decimals.");

            RuleFor(x => x.Price)
                .InclusiveBetween(Product.PricePattern.min, Product.PricePattern.max).WithMessage($"Price must be between {Product.PricePattern.min} and {Product.PricePattern.max}, with {Product.PricePattern.scale} decimals.");
        }
    }
}