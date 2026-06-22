using Microsoft.AspNetCore.Mvc;
using LekuTrans.Data;

namespace LekuTrans.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TestController : ControllerBase
{
    private readonly AppDbContext _db;

    public TestController(AppDbContext db)
    {
        _db = db;
    }

    [HttpGet("ping")]
    public IActionResult Ping()
    {
        return Ok("API is working!");
    }

    [HttpGet("db")]
    public IActionResult DbTest()
    {
        bool canConnect = _db.Database.CanConnect();
        return Ok(new { database = canConnect ? "connected" : "failed" });
    }
}