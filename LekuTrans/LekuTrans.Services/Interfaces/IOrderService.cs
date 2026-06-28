using LekuTrans.Data.Enums;
using LekuTrans.Data.Models;
using LekuTrans.Services.Models;

namespace LekuTrans.Services.Interfaces;

public interface IOrderService
{
    Task<Order> CreateOrderAsync(OrderDto dto);
    Task<IEnumerable<Order>> GetAllAsync();
    Task<Order?> GetByIdAsync(long id);
    Task<IEnumerable<Order>> GetByClientAsync(long clientId);
    Task<Order?> UpdateStatusAsync(long id, OrderStatus newStatus);
}