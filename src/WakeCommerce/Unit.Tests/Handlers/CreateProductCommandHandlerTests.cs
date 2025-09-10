using Catalog.Application.Products.Create;
using Catalog.Domain.Entities.Products;
using Catalog.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Unit.Tests.Handlers
{
    public class CreateProductCommandHandlerTests : BaseUnitTest
    {
        public CreateProductCommandHandlerTests()
            : base()
        {
        }

        [Fact]
        public async Task Handle_ShouldReturnFailure_WhenProductWithSameNameExists()
        {
            // Arrange
            using var context = new ApplicationDbContext(_dbOptions);
            context.Products.Add(new Product("ExistingProduct", 5, 10.0m, DateTime.UtcNow));
            await context.SaveChangesAsync();

            var handler = new CreateProductCommandHandler(context, _dateTimeProviderMock.Object);
            var command = new CreateProductCommand { Name = "ExistingProduct", Quantity = 10, Price = 15.0m };

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(result.IsFailure);
            Assert.Equal(ProductErrors.Conflict(command.Name).Code, result.Error.Code);
        }

        [Fact]
        public async Task Handle_ShouldCreateProduct_WhenNameDoesNotExist()
        {
            // Arrange
            using var context = new ApplicationDbContext(_dbOptions);
            var handler = new CreateProductCommandHandler(context, _dateTimeProviderMock.Object);
            var command = new CreateProductCommand { Name = "NewProduct", Quantity = 10, Price = 20.0m };

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(result.IsSuccess);

            var product = await context.Products.FirstOrDefaultAsync(p => p.Id == result.Value);
            Assert.NotNull(product);
            Assert.Equal(command.Name, product!.Name);
            Assert.Equal(command.Quantity, product.Quantity);
            Assert.Equal(command.Price, product.Price);
            Assert.Equal(_dateTimeProviderMock.Object.UtcNow, product.CreatedAt);
        }
    }
}