using LekuTrans.Data.Enums;
using LekuTrans.Data.Models;

namespace LekuTrans.Services.Interfaces;

public interface IOrderService
{
    Task<Order> CreateOrderAsync(long clientId, long clientCargoId, string pickupAddress, string deliveryAddress,
        DateTime? loadingDt, DateTime? unloadingDt, LoadingType loadingType, string recipientCompany,
        string? recipientPhone, string? recipientInn, string? trailerType, bool insurance,
        string? additionalConditions, string? notes);
    Task<IEnumerable<Order>> GetAllAsync();
    Task<Order?> GetByIdAsync(long id);
    Task<IEnumerable<Order>> GetByClientAsync(long clientId);
    Task<Order?> UpdateStatusAsync(long id, OrderStatus newStatus);
}