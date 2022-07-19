using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AngularWithIdentityServer.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class TestController : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetTestMessage()
    {
        return Ok("my message");
    }
}
