using Microsoft.AspNetCore.Mvc;

namespace JSL.CadCaminhoneiro.Api.Controllers.v1
{
    [Route("api/[controller]")]
    [ApiController]
    public class StartController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok("Service started successfully.");
        }
    }
}
