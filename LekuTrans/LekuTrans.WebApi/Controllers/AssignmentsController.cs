using Microsoft.AspNetCore.Mvc;
using LekuTrans.Services.Interfaces;
using LekuTrans.Services.Models;

namespace LekuTrans.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AssignmentsController : ControllerBase
{
    private readonly IAssignmentService _assignmentService;

    public AssignmentsController(IAssignmentService assignmentService)
    {
        _assignmentService = assignmentService;
    }

    [HttpPost]
    public async Task<IActionResult> AssignVehicle([FromBody] AssignVehicleDto dto)
    {
        var assignment = await _assignmentService.AssignVehicleAsync(dto.OrderId, dto.VehicleId, dto.DriverId);
        return CreatedAtAction(null, new { id = assignment.Id }, assignment);
    }

    [HttpPut("{id}/complete")]
    public async Task<IActionResult> Complete(long id)
    {
        try
        {
            await _assignmentService.CompleteAssignmentAsync(id);
            return NoContent();
        }
        catch (ArgumentNullException)
        {
            return NotFound($"Назначение с ID {id} не найдено.");
        }
    }
}