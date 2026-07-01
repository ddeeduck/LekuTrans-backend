using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using LekuTrans.Services.Interfaces;
using LekuTrans.Services.Models;

namespace LekuTrans.WebApi.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class VehiclesController : ControllerBase
{
    private readonly IVehicleService _vehicleService;

    public VehiclesController(IVehicleService vehicleService)
    {
        _vehicleService = vehicleService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var vehicles = await _vehicleService.GetAllAsync();
        return Ok(vehicles);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(long id)
    {
        var vehicle = await _vehicleService.GetByIdAsync(id);
        if (vehicle == null)
            return NotFound($"Машина с ID {id} не найдена.");
        return Ok(vehicle);
    }

    [HttpGet("available")]
    public async Task<IActionResult> GetAvailable()
    {
        var vehicles = await _vehicleService.GetAvailableAsync();
        return Ok(vehicles);
    }

    [Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<IActionResult> Add([FromBody] VehicleDto dto)
    {
        var vehicle = await _vehicleService.AddVehicleAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = vehicle.Id }, vehicle);
    }

    [Authorize(Roles = "Admin")]
    [HttpPut("{id}/status")]
    public async Task<IActionResult> UpdateStatus(long id, [FromBody] UpdateVehicleStatusDto dto)
    {
        try
        {
            var vehicle = await _vehicleService.UpdateStatusAsync(id, dto.Status);
            return Ok(vehicle);
        }
        catch (ArgumentNullException)
        {
            return NotFound($"Машина с ID {id} не найдена.");
        }
    }

    [Authorize(Roles = "Admin")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(long id)
    {
        try
        {
            await _vehicleService.DeleteVehicleAsync(id);
            return NoContent();
        }
        catch (ArgumentNullException)
        {
            return NotFound($"Машина с ID {id} не найдена.");
        }
    }
}