using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Mobo.Api.Controllers.v1
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