using Microsoft.AspNetCore.Mvc;
using LekuTrans.Services.Interfaces;

namespace LekuTrans.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;

    public UsersController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetProfile(long id)
    {
        var user = await _userService.GetProfileAsync(id);
        if (user == null)
            return NotFound($"Пользователь с ID {id} не найден.");
        return Ok(new { user.Id, user.Email, user.Role, user.FullName });
    }
}