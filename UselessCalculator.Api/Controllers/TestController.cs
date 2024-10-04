using Microsoft.AspNetCore.Mvc;

namespace UselessCalculator.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TestController : ControllerBase
{
    [HttpGet("throw")]
    public IActionResult ThrowException()
    {
        // This will trigger the exception handling middleware
        throw new InvalidOperationException("This is a test exception");
    }

    [HttpGet("notfound")]
    public IActionResult ThrowNotFoundException()
    {
        // Throwing a KeyNotFoundException to test 404 error handling
        throw new KeyNotFoundException("Resource not found");
    }
}