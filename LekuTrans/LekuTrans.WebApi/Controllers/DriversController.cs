using Microsoft.AspNetCore.Mvc;
using LekuTrans.Services.Interfaces;
using LekuTrans.Services.Models;
using LekuTrans.Data.Enums;

namespace LekuTrans.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DriversController : ControllerBase
{
    private readonly IDriverService _driverService;

    public DriversController(IDriverService driverService)
    {
        _driverService = driverService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var drivers = await _driverService.GetAllAsync();
        return Ok(drivers);
    }

    [HttpGet("available")]
    public async Task<IActionResult> GetAvailable()
    {
        var drivers = await _driverService.GetAvailableAsync();
        return Ok(drivers);
    }

    [HttpPost]
    public async Task<IActionResult> Add([FromBody] DriverDto dto)
    {
        var driver = await _driverService.AddDriverAsync(dto);
        return Ok(driver);
    }

    [HttpPut("{id}/status")]
    public async Task<IActionResult> UpdateStatus(long id, [FromBody] DriverStatus newStatus)
    {
        var driver = await _driverService.UpdateStatusAsync(id, newStatus);
        if (driver == null)
            return NotFound($"Водитель с ID {id} не найден.");
        return Ok(driver);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(long id)
    {
        await _driverService.DeleteDriverAsync(id);
        return NoContent();
    }
}