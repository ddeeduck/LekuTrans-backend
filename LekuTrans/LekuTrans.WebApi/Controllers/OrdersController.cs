using LekuTrans.Data.Enums;
using LekuTrans.Services.Interfaces;
using LekuTrans.Services.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class OrdersController : ControllerBase
{
    private readonly IOrderService _orderService;

    public OrdersController(IOrderService orderService)
    {
        _orderService = orderService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var orders = await _orderService.GetAllAsync();
        return Ok(orders);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(long id)
    {
        var order = await _orderService.GetByIdAsync(id);
        if (order == null)
            return NotFound($"Заказ с ID {id} не найден.");
        return Ok(order);
    }

    [HttpGet("client/{clientId}")]
    public async Task<IActionResult> GetByClient(long clientId)
    {
        var orders = await _orderService.GetByClientAsync(clientId);
        return Ok(orders);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] OrderDto dto)
    {
        var order = await _orderService.CreateOrderAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = order.Id }, order);
    }

    [HttpPut("{id}/status")]
    public async Task<IActionResult> UpdateStatus(long id, [FromBody] UpdateStatusDto dto)
    {
        try
        {
            var order = await _orderService.UpdateStatusAsync(id, Enum.Parse<OrderStatus>(dto.Status));
            return Ok(order);
        }
        catch (ArgumentNullException)
        {
            return NotFound($"Заказ с ID {id} не найден.");
        }
    }
}