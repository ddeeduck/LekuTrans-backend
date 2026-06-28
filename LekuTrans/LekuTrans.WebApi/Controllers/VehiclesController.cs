using Microsoft.AspNetCore.Mvc;
using LekuTrans.Services.Interfaces;
using LekuTrans.Services.Models;
using LekuTrans.Data.Enums;

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
        return Ok(vehicle);
    }

    [HttpPut("{id}/status")]
    public async Task<IActionResult> UpdateStatus(long id, [FromBody] VehicleStatus newStatus)
    {
        var vehicle = await _vehicleService.UpdateStatusAsync(id, newStatus);
        if (vehicle == null)
            return NotFound($"Машина с ID {id} не найдена.");
        return Ok(vehicle);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(long id)
    {
        await _vehicleService.DeleteVehicleAsync(id);
        return NoContent();
    }
}