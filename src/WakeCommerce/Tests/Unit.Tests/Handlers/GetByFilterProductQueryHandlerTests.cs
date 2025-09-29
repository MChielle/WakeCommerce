using Catalog.Application.Products.GetByFilter;
using Catalog.Domain.Entities.Products;
using Catalog.Infrastructure.Database;

namespace Unit.Tests.Handlers
{
    public class GetByFilterProductQueryHandlerTests : BaseUnitTest
    {
        public GetByFilterProductQueryHandlerTests()
            : base()
        {
        }

        [Fact]
        public async Task Handle_ShouldReturnFailure_WhenNoProductsMatch()
        {
            // Arrange
            using var context = new ApplicationDbContext(_dbOptions);
            var handler = new GetByFilterProductQueryHandler(context);
            var query = new GetByFilterProductQuery
            {
                SearchTerm = "NonExisting"
            };

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.True(result.IsFailure);
            Assert.Equal(ProductErrors.NotFound().Code, result.Error.Code);
        }

        [Fact]
        public async Task Handle_ShouldFilterBySearchTerm()
        {
            // Arrange
            using var context = new ApplicationDbContext(_dbOptions);
            context.Products.AddRange(
                new Product("Banana", 10, 2.5m, DateTime.UtcNow),
                new Product("Apple", 5, 3.0m, DateTime.UtcNow)
            );
            await context.SaveChangesAsync();

            var handler = new GetByFilterProductQueryHandler(context);
            var query = new GetByFilterProductQuery
            {
                SearchTerm = "Banana"
            };

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Single(result.Value.Products);
            Assert.Equal("Banana", result.Value.Products.First().Name);
        }

        [Fact]
        public async Task Handle_ShouldOrderByPriceDescending()
        {
            // Arrange
            using var context = new ApplicationDbContext(_dbOptions);
            context.Products.AddRange(
                new Product("Cheap", 10, 1.0m, DateTime.UtcNow),
                new Product("Expensive", 5, 10.0m, DateTime.UtcNow)
            );
            await context.SaveChangesAsync();

            var handler = new GetByFilterProductQueryHandler(context);
            var query = new GetByFilterProductQuery
            {
                SortColumn = "price",
                SortOrder = "desc"
            };

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal("Expensive", result.Value.Products.First().Name);
        }

        [Fact]
        public async Task Handle_ShouldApplyPagination()
        {
            // Arrange
            using var context = new ApplicationDbContext(_dbOptions);
            for (int i = 1; i <= 10; i++)
            {
                context.Products.Add(new Product($"Product {i}", i, i * 2.0m, DateTime.UtcNow));
            }
            await context.SaveChangesAsync();

            var handler = new GetByFilterProductQueryHandler(context);
            var query = new GetByFilterProductQuery
            {
                Page = 1,
                PageSize = 3,
                SortColumn = "name"
            };

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal(3, result.Value.Products.Count());
            Assert.Contains(result.Value.Products.First().Name, "Product 3");
        }
    }
}