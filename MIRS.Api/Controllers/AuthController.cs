using Microsoft.AspNetCore.Mvc;
using MIRS.Application.DTOs;
using MIRS.Application.Interfaces;

namespace MIRS.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("login")]
    public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
    {
        var user = await _authService.LoginAsync(loginDto);

        if (user == null) return Unauthorized("Invalid email or password");

        return Ok(user);
    }

    [HttpPost("register")]
    public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
    {
        var user = await _authService.RegisterAsync(registerDto);

        if (user == null) return BadRequest("Problem registering user");

        return Ok(user);
    }
}
