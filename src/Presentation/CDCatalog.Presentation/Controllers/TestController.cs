using Microsoft.AspNetCore.Mvc;

namespace CDCatalog.Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TestController : ControllerBase
{
    [HttpGet]
    public IActionResult Get()
    {
        return Ok(new { message = "API работает!" });
    }
}