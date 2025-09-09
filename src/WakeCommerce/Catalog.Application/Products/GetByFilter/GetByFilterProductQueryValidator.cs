using Catalog.Application.Products.GetByFilter;
using FluentValidation;

namespace Catalog.Application.Products.GetByFilterOrdered
{
    public class GetByFilterProductQueryValidator : AbstractValidator<GetByFilterProductQuery>
    {
        public GetByFilterProductQueryValidator()
        {
            When(x => x.Page.HasValue, () => {
                RuleFor(x => x.Page.Value)
                    .GreaterThanOrEqualTo(0).WithMessage("Page must be equal or greater than 0.");
            });

            When(x => x.PageSize.HasValue, () =>
            {
                RuleFor(x => x.PageSize)
                .GreaterThanOrEqualTo(0).WithMessage("Page size must be equal or greater than 0.");
            });
        }
    }
}