using Catalog.Application.Abstractions.Requests;
using FluentValidation;

namespace Catalog.Application.Products.GetById
{
    public class GetByIdProductQueryValidator : AbstractValidator<GetByIdProductQuery>
    {
        public GetByIdProductQueryValidator()
        {
            RuleFor(x => x.Id)
                .Must(x => !x.Equals(default)).WithMessage("Id is not valid.");
        }
    }
}