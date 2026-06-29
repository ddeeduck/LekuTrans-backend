using Microsoft.AspNetCore.Mvc;
using LekuTrans.Services.Interfaces;
using LekuTrans.Services.Models;

namespace LekuTrans.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReviewsController : ControllerBase
{
    private readonly IReviewService _reviewService;

    public ReviewsController(IReviewService reviewService)
    {
        _reviewService = reviewService;
    }

    [HttpGet("order/{orderId}")]
    public async Task<IActionResult> GetByOrder(long orderId)
    {
        var reviews = await _reviewService.GetByOrderAsync(orderId);
        return Ok(reviews);
    }

    [HttpPost]
    public async Task<IActionResult> Add([FromBody] AddReviewDto dto)
    {
        var review = await _reviewService.AddReviewAsync(dto.OrderId, dto.ClientId, dto.Rating, dto.Comment);
        return CreatedAtAction(nameof(GetByOrder), new { orderId = review.OrderId }, review);
    }
}