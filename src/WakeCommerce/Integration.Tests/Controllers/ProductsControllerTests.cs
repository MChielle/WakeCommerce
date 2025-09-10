using Catalog.Application.Products.Create;
using Catalog.Application.Products.GetByFilter;
using Catalog.Application.Products.UpdateById;
using System.Net;
using System.Net.Http.Json;
using System.Web;

namespace Integration.Tests.Controllers
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
        public async Task Get_GetProductById_InvalidParameter_ReturnsBadRequest()
        {
            // Arrange
            var productId = Guid.Parse("00000000-0000-0000-0000-000000000000"); //Invalid

            //Act
            var response = await _client.GetAsync($"/api/v1/products/{productId}/get-by-id");

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task Get_GetProductByFilter_WithoutParameters_ReturnsOk()
        {
            // Arrange NONE

            //Act
            var response = await _client.GetAsync($"/api/v1/products/get-by-filter");

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task Get_GetProductByFilter_FullParameters_ReturnsOk()
        {
            // Arrange
            var request = new GetByFilterProductQuery
            {
                Page = 0,
                PageSize = 1,
                SearchTerm = "Produto 1",
                SortColumn = "Price",
                SortOrder = "desc"
            };


            var url = "/api/v1/products/get-by-filter";
            var query = HttpUtility.ParseQueryString(string.Empty);
            query[nameof(request.Page)] = request.Page.ToString();
            query[nameof(request.PageSize)] = request.PageSize.ToString();
            query[nameof(request.SearchTerm)] = request.SearchTerm.ToString();
            query[nameof(request.SortColumn)] = request.SortColumn.ToString();
            query[nameof(request.SortOrder)] = request.SortOrder.ToString();
            if (query.HasKeys()) url += "?" + query.ToString();

            //Act
            var response = await _client.GetAsync(url);

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task Get_GetProductByFilter_ReturnsNotFound()
        {
            // Arrange
            var request = new GetByFilterProductQuery
            {
                SearchTerm = "Produto 999",
            };


            var url = "/api/v1/products/get-by-filter";
            var query = HttpUtility.ParseQueryString(string.Empty);
            query[nameof(request.SearchTerm)] = request.SearchTerm.ToString();
            if (query.HasKeys()) url += "?" + query.ToString();

            //Act
            var response = await _client.GetAsync(url);

            // Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
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
        public async Task Post_CreateNewProduct_ReturnsConflict()
        {
            // Arrange
            var product = new CreateProductCommand
            {
                Name = "Produto 1",
                Price = 1,
                Quantity = 1
            };

            var content = JsonContent.Create(product);

            //Act
            var response = await _client.PostAsync($"/api/v1/products/create-new-product", content);

            // Assert
            Assert.Equal(HttpStatusCode.Conflict, response.StatusCode);
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

        [Fact]
        public async Task Put_UpdateProductById_ReturnsOk()
        {
            // Arrange
            var productId = Guid.Parse("00000000-0000-0000-0000-000000000002");
            var product = new UpdateByIdProductCommand
            {
                Name = "Produto 100",
                Price = 10,
                Quantity = 10
            };

            var content = JsonContent.Create(product);

            //Act
            var response = await _client.PutAsync($"/api/v1/products/{productId}/update-by-id", content);

            // Assert
            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        }

        [Fact]
        public async Task Put_UpdateProductById_ReturnsConflict()
        {
            // Arrange
            var productId = Guid.Parse("00000000-0000-0000-0000-000000000003");
            var product = new UpdateByIdProductCommand
            {
                Name = "Produto 1",//Conflict
                Price = 1,
                Quantity = 1
            };

            var content = JsonContent.Create(product);

            //Act
            var response = await _client.PutAsync($"/api/v1/products/{productId}/update-by-id", content);

            // Assert
            Assert.Equal(HttpStatusCode.Conflict, response.StatusCode);
        }

        [Fact]
        public async Task Put_UpdateProductById_ValidationError_ReturnsBadRequest()
        {
            // Arrange
            var productId = Guid.Parse("00000000-0000-0000-0000-000000000000");
            var product = new UpdateByIdProductCommand
            {
                Name = "Test",
                Price = -1, //Invalid
                Quantity = -1 //Invalid
            };

            var content = JsonContent.Create(product);

            //Act
            var response = await _client.PutAsync($"/api/v1/products/{productId}/update-by-id", content);

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task Put_UpdateProductById_InvalidParameter_ReturnsBadRequest()
        {
            // Arrange
            var productId = Guid.Parse("10000000-0000-0000-0000-000000000001");
            var product = new UpdateByIdProductCommand
            {
                Name = "Test",
                Price = 1, //Invalid
                Quantity = 1 //Invalid
            };

            var content = JsonContent.Create(product);

            //Act
            var response = await _client.PutAsync($"/api/v1/products/{productId}/update-by-id", content);

            // Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task Delete_DeleteProductById_ReturnsOk()
        {
            // Arrange
            var productId = Guid.Parse("00000000-0000-0000-0000-000000000004");

            //Act
            var response = await _client.DeleteAsync($"/api/v1/products/{productId}/delete-by-id");

            // Assert
            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        }

        [Fact]
        public async Task Delete_DeleteProductById_ReturnsNotFount()
        {
            // Arrange
            var productId = Guid.Parse("10000000-0000-0000-0000-000000000001");

            //Act
            var response = await _client.DeleteAsync($"/api/v1/products/{productId}/delete-by-id");

            // Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task Delete_DeleteProductById_InvalidParameter_ReturnsOk()
        {
            // Arrange
            var productId = Guid.Parse("00000000-0000-0000-0000-000000000000");

            //Act
            var response = await _client.DeleteAsync($"/api/v1/products/{productId}/delete-by-id");

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }
    }
}