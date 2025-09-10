using Catalog.Application.Products.UpdateById;
using Catalog.Domain.Entities.Products;
using Catalog.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Unit.Tests
{
    public class UpdateByIdProductCommandHandlerTests : BaseUnitTest
    {
        public UpdateByIdProductCommandHandlerTests()
            : base()
        {
        }

        [Fact]
        public async Task Handle_ShouldReturnFailure_WhenProductNotFound()
        {
            // Arrange
            using var context = new ApplicationDbContext(_dbOptions);
            var handler = new UpdateByIdProductCommandHandler(context, _dateTimeProviderMock.Object);
            var command = new UpdateByIdProductCommand { Id = Guid.NewGuid(), Name = "NonExisting", Quantity = 10, Price = 20m };

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(result.IsFailure);
            Assert.Equal(ProductErrors.NotFound(command.Id).Code, result.Error.Code);
        }

        [Fact]
        public async Task Handle_ShouldReturnFailure_WhenNameConflictExists()
        {
            // Arrange
            using var context = new ApplicationDbContext(_dbOptions);
            var existing = new Product("ExistingName", 5, 10m, DateTime.UtcNow);
            var target = new Product("TargetProduct", 10, 20m, DateTime.UtcNow);
            context.Products.AddRange(existing, target);
            await context.SaveChangesAsync();

            var handler = new UpdateByIdProductCommandHandler(context, _dateTimeProviderMock.Object);
            var command = new UpdateByIdProductCommand { Id = target.Id, Name = "ExistingName", Quantity = 15, Price = 30m };

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(result.IsFailure);
            Assert.Equal(ProductErrors.Conflict(command.Name).Code, result.Error.Code);
        }

        [Fact]
        public async Task Handle_ShouldUpdateProduct_WhenValid()
        {
            // Arrange
            using var context = new ApplicationDbContext(_dbOptions);
            var product = new Product("OldName", 5, 10m, DateTime.UtcNow);
            context.Products.Add(product);
            await context.SaveChangesAsync();

            var handler = new UpdateByIdProductCommandHandler(context, _dateTimeProviderMock.Object);
            var command = new UpdateByIdProductCommand { Id = product.Id, Name = "NewName", Quantity = 99, Price = 199.99m };

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(result.IsSuccess);

            var updated = await context.Products.SingleOrDefaultAsync(p => p.Id == product.Id);
            Assert.NotNull(updated);
            Assert.Equal("NewName", updated!.Name);
            Assert.Equal(99, updated.Quantity);
            Assert.Equal(199.99m, updated.Price);
            Assert.Equal(_dateTimeProviderMock.Object.UtcNow, updated.UpdatedAt);
        }
    }
}