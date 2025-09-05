using Microsoft.AspNetCore.Mvc;

namespace Catalog.Web.Api.Controllers
{
    public class ProductController : ControllerBase
    {
        [HttpGet("api/products/{id}")]
        public IActionResult GetProductById(int id)
        {
            // Simulate fetching a product by ID
            var product = new { Id = id, Name = "Sample Product", Price = 19.99 };
            return Ok(product);
        }
    }
}
