using Catalog.Application.Products.GetById;
using Catalog.Domain.Entities.Products;
using Catalog.Infrastructure.Database;

namespace Unit.Tests
{
    public class GetByIdProductQueryHandlerTests : BaseUnitTest
    {
        public GetByIdProductQueryHandlerTests()
            : base()
        {
        }

        [Fact]
        public async Task Handle_ShouldReturnFailure_WhenProductNotFound()
        {
            // Arrange
            using var context = new ApplicationDbContext(_dbOptions);
            var handler = new GetByIdProductQueryHandler(context);
            var query = new GetByIdProductQuery(Guid.NewGuid());

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.True(result.IsFailure);
            Assert.Equal(ProductErrors.NotFound(query.Id).Code, result.Error.Code);
        }

        [Fact]
        public async Task Handle_ShouldReturnProduct_WhenProductExists()
        {
            // Arrange
            using var context = new ApplicationDbContext(_dbOptions);
            var product = new Product("Test Product", 10, 99.99m, DateTime.UtcNow);
            context.Products.Add(product);
            await context.SaveChangesAsync();

            var handler = new GetByIdProductQueryHandler(context);
            var query = new GetByIdProductQuery(product.Id);

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal(product.Name, result.Value.Name);
            Assert.Equal(product.Price, result.Value.Price);
            Assert.Equal(product.Quantity, result.Value.Quantity);
        }
    }
}