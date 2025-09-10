using Catalog.Application.Products.DeleteById;
using Catalog.Domain.Entities.Products;
using Catalog.Infrastructure.Database;

namespace Unit.Tests
{
    public class DeleteByIdProductCommandHandlerTests : BaseUnitTest
    {
        public DeleteByIdProductCommandHandlerTests()
            : base()
        {
        }

        [Fact]
        public async Task Handle_ShouldReturnFailure_WhenProductNotFound()
        {
            // Arrange
            using var context = new ApplicationDbContext(_dbOptions);
            var handler = new DeleteByIdProductCommandHandler(context, _dateTimeProviderMock.Object);
            var command = new DeleteByIdProductCommand(Guid.NewGuid());

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(result.IsFailure);
            Assert.Equal(ProductErrors.NotFound(command.Id).Code, result.Error.Code);
        }

        [Fact]
        public async Task Handle_ShouldDeleteProduct_WhenProductExists()
        {
            // Arrange
            using var context = new ApplicationDbContext(_dbOptions);
            var product = new Product("ToDelete", 5, 10.0m, DateTime.UtcNow);
            context.Products.Add(product);
            await context.SaveChangesAsync();

            var handler = new DeleteByIdProductCommandHandler(context, _dateTimeProviderMock.Object);
            var command = new DeleteByIdProductCommand(product.Id);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(result.IsSuccess);

            var deleted = await context.Products.FindAsync(product.Id);
            Assert.Null(deleted);
        }
    }
}