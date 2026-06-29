using LekuTrans.Data.Enums;
using LekuTrans.Services.Interfaces;
using LekuTrans.Services.Models;
using Microsoft.AspNetCore.Mvc;

namespace LekuTrans.WebApi.Controllers;

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

    [HttpPost]
    public async Task<IActionResult> Add([FromBody] VehicleDto dto)
    {
        var vehicle = await _vehicleService.AddVehicleAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = vehicle.Id }, vehicle);
    }

    [HttpPut("{id}/status")]
    public async Task<IActionResult> UpdateStatus(long id, [FromBody] UpdateStatusDto dto)
    {
        try
        {
            var vehicle = await _vehicleService.UpdateStatusAsync(id, Enum.Parse<VehicleStatus>(dto.Status));
            return Ok(vehicle);
        }
        catch (ArgumentNullException)
        {
            return NotFound($"Машина с ID {id} не найдена.");
        }
    }

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