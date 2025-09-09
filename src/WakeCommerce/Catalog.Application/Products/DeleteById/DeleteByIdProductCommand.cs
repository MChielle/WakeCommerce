using Catalog.Application.Abstractions.Requests;

namespace Catalog.Application.Products.DeleteById
{
    public sealed class DeleteByIdProductCommand : ICommand
    {
        public Guid Id { get; set; }

        public DeleteByIdProductCommand(Guid id)
        {
            Id = id;
        }
    }
}