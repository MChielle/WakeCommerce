using FluentValidation;

namespace Catalog.Application.Products.DeleteById
{
    public class DeleteByIdProductCommandValidator : AbstractValidator<DeleteByIdProductCommand>
    {
        public DeleteByIdProductCommandValidator()
        {
            RuleFor(x => x.Id)
                    .Must(x => !x.Equals(default)).WithMessage("Id is not valid.");
        }
    }
}