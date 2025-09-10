using AslaveCare.IntegrationTests.Configuration;
using Catalog.Application.Products.Create;
using Catalog.IntegrationTests;
using System.Net;
using System.Net.Http.Json;
using System.Security.Cryptography.Xml;

namespace Catalog.IntegrationTest.Controllers
{
    public class ProductsControllerTests : BaseIntegrationTest
    {

        public ProductsControllerTests(IntegrationTestWebApplicationFactory factory)
            : base(factory)
        {
        }

        [Fact]
        public async Task Get_GetProductById_ReturnsOk()
        {
            // Arrange
            var productId = Guid.Parse("00000000-0000-0000-0000-000000000001");

            //Act
            var response = await _client.GetAsync($"/api/v1/products/{productId}/get-by-id");

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task Get_GetProductById_ReturnsNotFound()
        {
            // Arrange
            var productId = Guid.Parse("10000000-0000-0000-0000-000000000001");

            //Act
            var response = await _client.GetAsync($"/api/v1/products/{productId}/get-by-id");

            // Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task Get_GetProductById_ReturnsBadRequest()
        {
            // Arrange
            var productId = Guid.Parse("00000000-0000-0000-0000-000000000000"); //Invalid

            //Act
            var response = await _client.GetAsync($"/api/v1/products/{productId}/get-by-id");

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task Get_GetProductByFilter_ReturnsOk()
        {
            // Arrange NONE

            //Act
            var response = await _client.GetAsync($"/api/v1/products/get-by-filter");

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task Post_CreateNewProduct_ReturnsOk()
        {
            // Arrange 
            var product = new CreateProductCommand
            {
                Name = "Test",
                Price = 1,
                Quantity = 1
            };

            var content = JsonContent.Create(product);

            //Act
            var response = await _client.PostAsync($"/api/v1/products/create-new-product", content);

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task Post_CreateNewProduct_ValidationError_ReturnsBadRequest()
        {
            // Arrange 
            var product = new CreateProductCommand
            {
                Name = "Test",
                Price = -1, //Invalid
                Quantity = -1 //Invalid
            };

            var content = JsonContent.Create(product);

            //Act
            var response = await _client.PostAsync($"/api/v1/products/create-new-product", content);

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }
    }
}