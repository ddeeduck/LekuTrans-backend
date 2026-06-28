using Microsoft.AspNetCore.Mvc;
using LekuTrans.Services.Interfaces;
using LekuTrans.Services.Models;

namespace LekuTrans.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IUserService _userService;

    public AuthController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] UserDto dto)
    {
        var user = await _userService.RegisterAsync(dto);
        return Ok(new { user.Id, user.Email, user.Role, user.FullName });
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto dto)
    {
        var user = await _userService.LoginAsync(dto.Email, dto.PasswordHash);
        if (user == null)
            return Unauthorized("Неверный email или пароль.");
        return Ok(new { user.Id, user.Email, user.Role, user.FullName });
    }
}