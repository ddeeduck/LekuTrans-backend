using LekuTrans.Data.Enums;
using LekuTrans.Services.Interfaces;
using LekuTrans.Services.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

[Authorize]
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
        return CreatedAtAction(nameof(GetAll), new { id = driver.Id }, driver);
    }

    [HttpPut("{id}/status")]
    public async Task<IActionResult> UpdateStatus(long id, [FromBody] UpdateStatusDto dto)
    {
        try
        {
            var driver = await _driverService.UpdateStatusAsync(id, Enum.Parse<DriverStatus>(dto.Status));
            return Ok(driver);
        }
        catch (ArgumentNullException)
        {
            return NotFound($"Водитель с ID {id} не найден.");
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(long id)
    {
        try
        {
            await _driverService.DeleteDriverAsync(id);
            return NoContent();
        }
        catch (ArgumentNullException)
        {
            return NotFound($"Водитель с ID {id} не найден.");
        }
    }
}