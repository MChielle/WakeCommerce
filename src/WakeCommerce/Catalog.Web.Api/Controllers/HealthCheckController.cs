using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.Web.Api.Controllers
{
    [Route("api/[controller]")]
    public class HealthCheckController : ControllerBase
    {
        [AllowAnonymous]
        [HttpGet()]
        public async Task<IActionResult> Get()
        {
            return await Task.Run(() => Ok(new { message = "WakeCommerce Api is Online!" }));
        }
    }
}