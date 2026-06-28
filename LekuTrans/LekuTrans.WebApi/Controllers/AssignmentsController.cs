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
        return Ok(assignment);
    }

    [HttpPut("{id}/complete")]
    public async Task<IActionResult> Complete(long id)
    {
        await _assignmentService.CompleteAssignmentAsync(id);
        return NoContent();
    }
}