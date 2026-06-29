using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using LekuTrans.Services.Interfaces;
using LekuTrans.Services.Models;

namespace LekuTrans.WebApi.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class FeedbackController : ControllerBase
{
    private readonly IFeedbackService _feedbackService;

    public FeedbackController(IFeedbackService feedbackService)
    {
        _feedbackService = feedbackService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var feedbacks = await _feedbackService.GetAllAsync();
        return Ok(feedbacks);
    }

    [HttpPost]
    public async Task<IActionResult> Add([FromBody] AddFeedbackDto dto)
    {
        var feedback = await _feedbackService.AddFeedbackAsync(dto.UserId, dto.Type, dto.Name, dto.Email, dto.Message);
        return CreatedAtAction(nameof(GetAll), null, feedback);
    }
}