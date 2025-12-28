using Microsoft.AspNetCore.Mvc;
using MIRS.Application.Interfaces;

namespace MIRS.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class TestController:ControllerBase
{
    private readonly ITestAppService _testAppService;

    public TestController(ITestAppService testAppService)
    {
        _testAppService = testAppService;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetTests()
    {
        var tests = await _testAppService.GetTestsAsync();
        return Ok(tests);
    }
}